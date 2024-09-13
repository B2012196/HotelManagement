namespace PaymentManagement.API.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public PaymentStatus Status { get; set; }

        // Navigation property
        [JsonIgnore]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
