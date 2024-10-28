namespace Hotel.Web.Pages
{
    public class BookinghistoryModel(IBookingService bookingService, IGuestService guestService, 
        IHotelService hotelService, ILogger<BookinghistoryModel> logger) : PageModel
    {
        public IEnumerable<BookingView> BookingList { get; set; } = new List<BookingView>();
        public IEnumerable<BookingRoom> BookingRoomList { get; set; } = new List<BookingRoom>();
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                //get all bookingroom
                var resultbroom = await bookingService.GetBookingRooms();
                BookingRoomList = resultbroom.BookingRooms;

                //get all room
                var resultroom = await hotelService.GetRooms();
                RoomList = resultroom.Rooms;

                var token = HttpContext.Session.GetString("AccessToken");
                if (token != null)
                {
                    string userId = GetUserIdFromToken(token);
                    if (userId != null)
                    {
                        //get guest by userid
                        var resultstaff = await guestService.GetGuestByUserId(Guid.Parse(userId));
                        if (resultstaff != null)
                        {
                            //get bookings by guestid
                            var resultbookings = await bookingService.GetBookingsByGuestId(resultstaff.Guest.GuestId);
                            if (resultbookings != null)
                            {
                                var bookingViewList = new List<BookingView>();
                                //danh sach task api
                                var guestTasks = resultbookings.Bookings.Select(b => guestService.GetGuestById(b.GuestId)).ToList();
                                var typeTasks = resultbookings.Bookings.Select(b => hotelService.GetRoomTypeById(b.TypeId)).ToList();
                                //thuc hien task dong thoi
                                var guests = await Task.WhenAll(guestTasks);
                                var types = await Task.WhenAll(typeTasks);
                                //chuyen tu IEnumrable -> List
                                var bookingsList = resultbookings.Bookings.ToList();

                                for (int i = 0; i < resultbookings.Bookings.Count(); i++)
                                {
                                    var booking = bookingsList[i];
                                    var guest = guests[i];
                                    var type = types[i];
                                    if (guest == null || guest.Guest == null || type == null || type.RoomType == null)
                                    {
                                        logger.LogWarning($"Missing data for booking {booking.BookingId}");
                                        continue; // Skip this iteration if any necessary data is missing
                                    }

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
                                        TotalPrice = booking.TotalPrice,
                                    };

                                    var bookingroom = BookingRoomList.Where(b => b.BookingId == booking.BookingId).ToList();

                                    var roomnumber = RoomList.SingleOrDefault(r => r.RoomId == bookingroom[0].RoomId);
                                    if (roomnumber != null)
                                    {
                                        bookingView.RoomNumber = roomnumber.Number;
                                    }

                                    bookingViewList.Add(bookingView);
                                }
                                BookingList = bookingViewList;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogInformation($"{ex.Message}");
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
