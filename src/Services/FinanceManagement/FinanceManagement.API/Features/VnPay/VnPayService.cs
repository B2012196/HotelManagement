namespace FinanceManagement.API.Features.VnPay
{
    public class VnPayService(IConfiguration config, ILogger<VnPayService> logger) : IVnPayService
    {
        public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
        {
            if (model == null || model.Amount <= 0 || string.IsNullOrEmpty(model.InvoiceId.ToString()))
            {
                throw new ArgumentException("Thông tin thanh toán không hợp lệ.");
            }
            //var tick = DateTime.Now.Ticks.ToString();

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", config["VnPay:Version"]!);
            vnpay.AddRequestData("vnp_Command", config["VnPay:Command"]!);
            vnpay.AddRequestData("vnp_TmnCode", config["VnPay:TmnCode"]!);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", config["VnPay:CurrCode"]!);
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1"); //Utils.GetIpAddress(context)
            vnpay.AddRequestData("vnp_Locale", config["VnPay:Locale"]!);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.InvoiceId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", config["VnPay:ReturnUrl"]!);
            vnpay.AddRequestData("vnp_TxnRef", model.InvoiceId.ToString());

            var paymentUrl = vnpay.CreateRequestUrl(config["VnPay:BaseUrl"]!, config["VnPay:HashSecret"]!);

            return paymentUrl;
        }


        public async Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();

            foreach(var (key, value) in collections)
            {
                if(!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                    logger.LogWarning("vnpay "+key + " - " + value.ToString());
                }
            }

            // Kiểm tra và chuyển đổi các giá trị an toàn
            Guid vnp_InvoiceId = Guid.Empty;
            long vnp_TransactionId = 0;

            if (Guid.TryParse(vnpay.GetResponseData("vnp_TxnRef"), out var invoiceId))
            {
                vnp_InvoiceId = invoiceId;
            }
            else
            {
                // Log lỗi hoặc xử lý khi không có giá trị hợp lệ
                logger.LogWarning("vnp_TxnRef không có giá trị hợp lệ.");
            }

            if (long.TryParse(vnpay.GetResponseData("vnp_TransactionStatus"), out var transactionId))
            {
                vnp_TransactionId = transactionId;
            }
            else
            {
                // Log lỗi hoặc xử lý khi không có giá trị hợp lệ
                logger.LogWarning("vnp_TransactionNo không có giá trị hợp lệ.");
            }
            var vnp_SecureHash = collections.FirstOrDefault(s => s.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_InvoiceInfo = vnpay.GetResponseData("vnp_InvoiceInfo");

            logger.LogWarning("HashSecret: " + vnp_SecureHash + "  " + config["VnPay:HashSecret"]!);

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, config["VnPay:HashSecret"]!);

            logger.LogWarning("checkSignature: " + checkSignature);
            if (!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }

            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                InvoiceDescription = vnp_InvoiceInfo,
                InvoiceId = vnp_InvoiceId,
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
            };
        }
    }
}
