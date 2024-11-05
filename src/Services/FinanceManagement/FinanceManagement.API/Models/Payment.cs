namespace FinanceManagement.API.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateAt { get; set; }

        [JsonIgnore]
        public Invoice Invoice { get; set; }
        [JsonIgnore]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
