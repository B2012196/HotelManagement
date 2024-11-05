namespace FinanceManagement.API.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid BookingId { get; set; }
        public Guid GuestId { get; set;}
        public DateTime CreateAt { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public decimal? TotalPrice { get; set; }

        [JsonIgnore]
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        [JsonIgnore]
        public ICollection<Payment> Payments { get; set; }
    }
}
