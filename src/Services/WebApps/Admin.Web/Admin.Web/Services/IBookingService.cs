namespace Admin.Web.Services
{
    public interface IBookingService
    {
        [Get("/bookings/bookings")]
        Task<GetBookingsResponse> GetBookings();

        [Post("/bookings/bookings")]
        Task<CreateBookingResponse> CreateBooking(Booking Booking);

        [Put("/bookings/confirm")]
        Task<UpdateBookingConfirmResponse> UpdateBookingConfirm(object obj);

        [Put("/bookings/checkin")]
        Task<UpdateBookingCheckinResponse> UpdateBookingCheckin(Booking Booking);

        [Put("/bookings/checkout")]
        Task<UpdateBookingCheckoutResponse> UpdateBookingCheckout(Booking Booking);

        [Delete("/bookings/bookings/{BookingId}")]
        Task<DeleteBookingResponse> DeleteBooking(Guid BookingId);
    }
}
