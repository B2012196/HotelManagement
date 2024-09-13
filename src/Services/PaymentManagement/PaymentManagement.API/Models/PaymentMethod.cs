namespace PaymentManagement.API.Models
{
    public class PaymentMethod
    {
        public Guid PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }

        // Navigation property
        [JsonIgnore]
        public ICollection<Payment> Payments { get; set; }  

    }
}
