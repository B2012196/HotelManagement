namespace Admin.Web.Models
{
    public class Ordering
    {
        public Guid OrderingId { get; set; }
        public Guid BookingId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CreateAt { get; set; }
        public OrderingStatus OrderingStatus { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public record CreateOrderingResponse(Guid OrderingId);
}
