namespace Admin.Web.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateAt { get; set; }
    }
    public record GetPaymentsResponse(IEnumerable<Payment> Payments);
    public record GetPayByInvoiceIdResponse(Payment Payment);
    public record CreatePaymentResponse(string PaymentUrl, Guid PaymentId);

    public record CreatePayDirectResponse(Guid PaymentId);
}
