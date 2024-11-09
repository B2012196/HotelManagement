namespace FinanceManagement.API.Models
{
    public class InvoiceDetail
    {
        public Guid DetailId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid ServiceId { get; set; }
        public int Numberofservice { get; set; }
        public decimal TotalPrice { get; set; }

        [JsonIgnore]
        public Service Service { get; set; }

        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}
