
namespace FinanceManagement.API.Models
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public byte[]? ServiceImage { get; set; }
        public string? ContentImage { get; set; }

        [JsonIgnore]
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
