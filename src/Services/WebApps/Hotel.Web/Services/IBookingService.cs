namespace Hotel.Web.Services
{
    public interface IBookingService
    {
        [Get("/bookings/bookings")]
        Task<GetBookingsResponse> GetBookings();

        [Get("/bookings/bookings/guestid/{GuestId}")]
        Task<GetBookingsResponse> GetBookingsByGuestId(Guid GuestId);

        [Post("/bookings/bookings")]
        Task<CreateBookingResponse> CreateBooking(Booking Booking);

        [Put("/bookings/confirm")]
        Task<UpdateBookingConfirmResponse> UpdateBookingConfirm(Booking Booking);

        [Put("/bookings/checkin")]
        Task<UpdateBookingCheckinResponse> UpdateBookingCheckin(Booking Booking);

        [Put("/bookings/checkout")]
        Task<UpdateBookingCheckoutResponse> UpdateBookingCheckout(Booking Booking);

        [Delete("/bookings/bookings/{BookingId}")]
        Task<DeleteHotelResponse> DeleteBooking(Guid BookingId);
    }
}
