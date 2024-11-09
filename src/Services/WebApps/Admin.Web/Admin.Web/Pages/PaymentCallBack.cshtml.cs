using Microsoft.Extensions.Logging;

namespace Admin.Web.Pages
{
    public class PaymentCallBackModel(IFinanceService financeService, ILogger<PaymentCallBackModel> logger) : PageModel
    {
        public bool IsPaymentSuccessful { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Nhận các tham số từ VnPay
            // Chuyển đổi IQueryCollection từ Request.Query thành Dictionary<string, string>
            var paymentRequest = new PaymentExecuteRequest
            {
                VnpAmount = Request.Query["vnp_Amount"]!,
                VnpBankCode = Request.Query["vnp_BankCode"]!,
                VnpBankTranNo = Request.Query["vnp_BankTranNo"]!,
                VnpCardType = Request.Query["vnp_CardType"]!,
                VnpOrderInfo = Request.Query["vnp_OrderInfo"]!,
                VnpPayDate = Request.Query["vnp_PayDate"]!,
                VnpResponseCode = Request.Query["vnp_ResponseCode"]!,
                VnpTmnCode = Request.Query["vnp_TmnCode"]!,
                VnpTransactionNo = Request.Query["vnp_TransactionNo"]!,
                VnpTransactionStatus = Request.Query["vnp_TransactionStatus"]!,
                VnpTxnRef = Request.Query["vnp_TxnRef"]!,
                VnpSecureHash = Request.Query["vnp_SecureHash"]!
            };

            // Sử dụng ILogger để log ra các giá trị của paymentRequest
            logger.LogInformation("Payment Request Values:");
            logger.LogInformation("VnpAmount: {VnpAmount}", paymentRequest.VnpAmount);
            logger.LogInformation("VnpBankCode: {VnpBankCode}", paymentRequest.VnpBankCode);
            logger.LogInformation("VnpBankTranNo: {VnpBankTranNo}", paymentRequest.VnpBankTranNo);
            logger.LogInformation("VnpCardType: {VnpCardType}", paymentRequest.VnpCardType);
            logger.LogInformation("VnpOrderInfo: {VnpOrderInfo}", paymentRequest.VnpOrderInfo);
            logger.LogInformation("VnpPayDate: {VnpPayDate}", paymentRequest.VnpPayDate);
            logger.LogInformation("VnpResponseCode: {VnpResponseCode}", paymentRequest.VnpResponseCode);
            logger.LogInformation("VnpTmnCode: {VnpTmnCode}", paymentRequest.VnpTmnCode);
            logger.LogInformation("VnpTransactionNo: {VnpTransactionNo}", paymentRequest.VnpTransactionNo);
            logger.LogInformation("VnpTransactionStatus: {VnpTransactionStatus}", paymentRequest.VnpTransactionStatus);
            logger.LogInformation("VnpTxnRef: {VnpTxnRef}", paymentRequest.VnpTxnRef);
            logger.LogInformation("VnpSecureHash: {VnpSecureHash}", paymentRequest.VnpSecureHash);

            // Gọi FinanceService để xác thực và xử lý kết quả giao dịch
            var response = await financeService.PaymentExecute(paymentRequest);

            // Kiểm tra kết quả từ FinanceService
            logger.LogWarning(response.VnPaymentResponseModel.Success + "");
            logger.LogWarning(response.VnPaymentResponseModel.VnPayResponseCode + "");
            if (response.VnPaymentResponseModel.Success)
            {
                IsPaymentSuccessful = true;
                return Page(); // Hiển thị kết quả thành công trên Razor Page
            }
            else
            {
                IsPaymentSuccessful = false;
                return Page(); // Hiển thị kết quả thất bại trên Razor Page
            }
        }
    }
}
