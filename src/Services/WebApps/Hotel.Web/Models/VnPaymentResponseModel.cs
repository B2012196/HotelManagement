namespace Hotel.Web.Models
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string InvoiceDescription { get; set; }
        public Guid InvoiceId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }

    public record PaymentExecuteResponse(VnPaymentResponseModel VnPaymentResponseModel);
    public record CreatePaymentResponse(string PaymentUrl, Guid PaymentId);
}
