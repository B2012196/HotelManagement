namespace Admin.Web.Pages
{
    public class BookingModel(IBookingService bookingService, IGuestService guestService, IHotelService hotelService, ILogger<BookingModel> logger) : PageModel
    {
        public IEnumerable<BookingView> BookingList { get; set; } = new List<BookingView>();
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultbooking = await bookingService.GetBookings();
                var bookingViewList = new List<BookingView>();

                foreach (var booking in resultbooking.Bookings)
                {
                    var guest = await guestService.GetGuestById(booking.GuestId);
                    var type = await hotelService.GetRoomTypeById(booking.TypeId);
                    var bookingView = new BookingView
                    {
                        BookingId = booking.BookingId,
                        TypeId = booking.TypeId,
                        TypeName = type.RoomType.Name,
                        GuestFirstName = guest.Guest.FirstName,
                        GuestLastName = guest.Guest.LastName,
                        ExpectedCheckinDate = booking.ExpectedCheckinDate,
                        ExpectedCheckoutDate = booking.ExpectedCheckoutDate,
                        CheckinDate = booking.CheckinDate,
                        CheckoutDate = booking.CheckoutDate,
                        RoomQuantity = booking.RoomQuantity,
                        BookingStatus = booking.BookingStatus,
                    };
                    bookingViewList.Add(bookingView);
                }
                BookingList = bookingViewList;

                var resultroom = await hotelService.GetRooms();
                RoomList = resultroom.Rooms;

            }
            catch (Exception ex)
            {
                logger.LogInformation($"{ex.Message}");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostConfirmBookingAsync(string BookingId, string RoomId)
        {
            Guid bookingIdGuid;
            Guid roomIdGuid;
            if (!Guid.TryParse(BookingId, out bookingIdGuid) || !Guid.TryParse(RoomId, out roomIdGuid))
            {
                ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                logger.LogInformation("Dữ liệu không hợp lệ.");
                return Page();
            }

            var confirm = new
            {
                BookingId = bookingIdGuid,
                RoomId = roomIdGuid
            };

            var resultconfirm = await bookingService.UpdateBookingConfirm(confirm);

            if(!resultconfirm.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update confirm the booking");
            }

            return RedirectToPage("Booking");
        }
    }
}
