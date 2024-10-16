namespace Hotel.Web.Pages
{
    public class BookinghistoryModel(IBookingService bookingService, IGuestService guestService, IHotelService hotelService) : PageModel
    {
        public IEnumerable<BookingView> BookingList { get; set; } = new List<BookingView>();
        public async Task<IActionResult> OnGetAsync()
        {

            var token = HttpContext.Session.GetString("AccessToken");
            if (token != null)
            {
                string userId = GetUserIdFromToken(token);
                if (userId != null)
                {
                    var resultstaff = await guestService.GetGuestByUserId(Guid.Parse(userId));
                    if(resultstaff != null)
                    {
                        var resultbookings = await bookingService.GetBookingsByGuestId(resultstaff.Guest.GuestId);
                        if(resultbookings != null)
                        {
                            var bookingViewList = new List<BookingView>();
                            foreach (var booking in resultbookings.Bookings)
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
                        }
                    }
                }
            }
            return Page();
        }

        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token); // Đọc JWT token

            // Lấy claim 'userid' từ token
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userid");

            if (userIdClaim != null)
            {
                return userIdClaim.Value;  // Trả về giá trị userid
            }

            return null; // Nếu không tìm thấy claim userid
        }
    }
}
