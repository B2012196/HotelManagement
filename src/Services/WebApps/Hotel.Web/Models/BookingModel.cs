namespace Hotel.Web.Models
{
    public class BookingView
    {
        public Guid BookingId { get; set; }
        public string BookingCode { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public DateTime ExpectedCheckinDate { get; set; }
        public DateTime ExpectedCheckoutDate { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int RoomQuantity { get; set; }
        public string RoomNumber { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }

    public class Booking
    {
        public Guid BookingId { get; set; }
        public string BookingCode { get; set; }
        public Guid GuestId { get; set; }
        public Guid TypeId { get; set; }
        public DateTime ExpectedCheckinDate { get; set; }
        public DateTime ExpectedCheckoutDate { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int RoomQuantity { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }

    public record GetBookingsResponse(IEnumerable<Booking> Bookings);
    public record CreateBookingResponse(Guid BookingId);
    public record UpdateBookingConfirmResponse(bool IsSuccess);
    public record UpdateBookingCheckinResponse(bool IsSuccess);
    public record UpdateBookingCheckoutResponse(bool IsSuccess);
    public record DeleteBookingResponse(bool IsSuccess);
}
