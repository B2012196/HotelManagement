namespace Hotel.Web.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public Guid GuestId { get; set; }
        public Guid TypeId {get; set; }
        public DateTime ExpectedCheckinDate { get; set; }
        public DateTime ExpectedCheckoutDate { get; set; }
        public int RoomQuantity { get; set; }
    }
    public record GetBookingsResponse(IEnumerable<Booking> Bookings);
    public record CreateBookingResponse(Guid BookingId);
    public record UpdateBookingConfirmResponse(bool IsSuccess);
    public record UpdateBookingCheckinResponse(bool IsSuccess);
    public record UpdateBookingCheckoutResponse(bool IsSuccess);
    public record DeleteBookingResponse(bool IsSuccess);
}
