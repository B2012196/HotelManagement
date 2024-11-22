namespace Admin.Web.Services
{
    public interface IBookingService
    {
        [Get("/bookings/bookings")]
        Task<GetBookingsResponse> GetBookings(int? pageNumber, int? pageSize);

        [Get("/bookings/bookingrooms")]
        Task<GetBookingRoomsResponse> GetBookingRooms();

        [Post("/bookings/bookings")]
        Task<CreateBookingResponse> CreateBooking(Booking Booking);

        [Put("/bookings/bookings/confirm")]
        Task<UpdateBookingConfirmResponse> UpdateBookingConfirm(object obj);

        [Put("/bookings/bookings/checkin")]
        Task<UpdateBookingCheckinResponse> UpdateBookingCheckin(object obj);

        [Put("/bookings/bookings/checkout")]
        Task<UpdateBookingCheckoutResponse> UpdateBookingCheckout(object obj);

        [Delete("/bookings/bookings/{BookingId}")]
        Task<DeleteBookingResponse> DeleteBooking(Guid BookingId);
    }
}
