namespace FinanceManagement.API.Models
{
    public class PaymentMethod
    {
        public Guid PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }

        [JsonIgnore]
        public ICollection<Payment> Payments { get; set; }
    }
}
