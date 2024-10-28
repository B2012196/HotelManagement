namespace FinanceManagement.API.Models
{
    public class Ordering
    {
        public Guid OrderingId { get; set; }
        public Guid BookingId { get; set; }
        public Guid GuestId { get; set;}
        public DateTime CreateAt { get; set; }
        public OrderingStatus OrderingStatus { get; set; }
        public decimal? TotalPrice { get; set; }

        [JsonIgnore]
        public ICollection<OrderingDetail> OrderingDetails { get; set; }
        [JsonIgnore]
        public ICollection<Payment> Payments { get; set; }
    }
}
