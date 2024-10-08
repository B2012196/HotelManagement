namespace Admin.Web.Pages
{
    public class BookingModel(IBookingService bookingService, IGuestService guestService,ILogger<BookingModel> logger) : PageModel
    {
        public IEnumerable<BookingView> BookingList { get; set; } = new List<BookingView>();
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultbooking = await bookingService.GetBookings();
                var bookingViewList = new List<BookingView>();

                foreach (var booking in resultbooking.Bookings)
                {
                    var guest = await guestService.GetGuestById(booking.GuestId);

                    var bookingView = new BookingView
                    {
                        BookingId = booking.BookingId,
                        GuestFirstName = guest.Guest.FirstName,
                        GuestLastName = guest.Guest.LastName,
                        ExpectedCheckinDate = booking.ExpectedCheckinDate,
                        ExpectedCheckoutDate = booking.ExpectedCheckoutDate,
                        CheckinDate = booking.CheckinDate,
                        CheckoutDate = booking.CheckoutDate,
                        RoomQuantity = booking.RoomQuantity
                    };
                    bookingViewList.Add(bookingView);
                }
                BookingList = bookingViewList;

            }
            catch (Exception ex)
            {
                logger.LogInformation($"{ex.Message}");
            }
            return Page();
        }


    }
}
