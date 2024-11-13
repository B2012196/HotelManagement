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
                vnp_Amount = Request.Query["vnp_Amount"]!,
                vnp_BankCode = Request.Query["vnp_BankCode"]!,
                vnp_BankTranNo = Request.Query["vnp_BankTranNo"]!,
                vnp_CardType = Request.Query["vnp_CardType"]!,
                vnp_OrderInfo = Request.Query["vnp_OrderInfo"]!,
                vnp_PayDate = Request.Query["vnp_PayDate"]!,
                vnp_ResponseCode = Request.Query["vnp_ResponseCode"]!,
                vnp_TmnCode = Request.Query["vnp_TmnCode"]!,
                vnp_TransactionNo = Request.Query["vnp_TransactionNo"]!,
                vnp_TransactionStatus = Request.Query["vnp_TransactionStatus"]!,
                vnp_TxnRef = Request.Query["vnp_TxnRef"]!,
                vnp_SecureHash = Request.Query["vnp_SecureHash"]!
            };

            // Sử dụng ILogger để log ra các giá trị của paymentRequest
            logger.LogInformation("Payment Request Values:");
            logger.LogInformation("VnpAmount: {VnpAmount}", paymentRequest.vnp_Amount);
            logger.LogInformation("VnpBankCode: {VnpBankCode}", paymentRequest.vnp_BankCode);
            logger.LogInformation("VnpBankTranNo: {VnpBankTranNo}", paymentRequest.vnp_BankTranNo);
            logger.LogInformation("VnpCardType: {VnpCardType}", paymentRequest.vnp_CardType);
            logger.LogInformation("VnpOrderInfo: {VnpOrderInfo}", paymentRequest.vnp_OrderInfo);
            logger.LogInformation("VnpPayDate: {VnpPayDate}", paymentRequest.vnp_PayDate);
            logger.LogInformation("VnpResponseCode: {VnpResponseCode}", paymentRequest.vnp_ResponseCode);
            logger.LogInformation("VnpTmnCode: {VnpTmnCode}", paymentRequest.vnp_TmnCode);
            logger.LogInformation("VnpTransactionNo: {VnpTransactionNo}", paymentRequest.vnp_TransactionNo);
            logger.LogInformation("VnpTransactionStatus: {VnpTransactionStatus}", paymentRequest.vnp_TransactionStatus);
            logger.LogInformation("VnpTxnRef: {VnpTxnRef}", paymentRequest.vnp_TxnRef);
            logger.LogInformation("VnpSecureHash: {VnpSecureHash}", paymentRequest.vnp_SecureHash);

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
