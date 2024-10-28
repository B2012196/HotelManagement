namespace FinanceManagement.API.Models
{
    public class OrderingDetail
    {
        public Guid OrderingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Numberofservice { get; set; }
        public decimal TotalPrice { get; set; }

        [JsonIgnore]
        public Service Service { get; set; }

        [JsonIgnore]
        public Ordering Ordering { get; set; }
    }
}
