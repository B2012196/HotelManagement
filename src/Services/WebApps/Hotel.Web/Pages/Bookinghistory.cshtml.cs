﻿namespace Hotel.Web.Pages
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
                                        BookingCode = booking.BookingCode,
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

                                    var bookingrooms = BookingRoomList.Where(b => b.BookingId == booking.BookingId).ToList();
                                    bookingView.RoomNumber = "";
                                    foreach (var bookroom in bookingrooms)
                                    {
                                        var roomnumber = RoomList.SingleOrDefault(r => r.RoomId == bookroom.RoomId);
                                        if(roomnumber != null)
                                        {
                                            bookingView.RoomNumber += roomnumber.Number + " ";
                                        }
                                    }

                                    bookingViewList.Add(bookingView);
                                }
                                BookingList = bookingViewList;
                            }
                        }
                    }
                }
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return Page();
        }
        private void HandleApiException(ApiException apiEx)
        {
            switch (apiEx.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                    break;

                case System.Net.HttpStatusCode.Unauthorized:
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                    break;

                case System.Net.HttpStatusCode.Forbidden:
                    Console.WriteLine("Forbidden: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                    break;

                default:
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                    break;
            }
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
