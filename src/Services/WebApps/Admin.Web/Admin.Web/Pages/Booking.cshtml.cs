using Admin.Web.Services;

namespace Admin.Web.Pages
{
    public class BookingModel(IBookingService bookingService, IGuestService guestService, IHotelService hotelService, IFinanceService financeService,
        ILogger<BookingModel> logger) : PageModel
    {
        public IEnumerable<BookingView> BookingList { get; set; } = new List<BookingView>();
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
        public IEnumerable<Service> ServiceList { get; set; } = new List<Service>();
        public IEnumerable<RoomType> RoomTypeList { get; set; } = new List<RoomType>();
        public IEnumerable<BookingRoom> BookingRoomList { get; set; } = new List<BookingRoom>();
        public Room BRoom { get; set; } = new Room();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                //get all booking
                var resultbooking = await bookingService.GetBookings();

                //get all guest
                var resultguest = await guestService.GetGuests();
                GuestList = resultguest.Guests;

                //get all roomtype
                var resultroomtype = await hotelService.GetRoomTypes();
                RoomTypeList = resultroomtype.RoomTypes;

                //get all room
                var resultroom = await hotelService.GetRooms();
                RoomList = resultroom.Rooms;

                //get all bookingroom
                var resultbroom = await bookingService.GetBookingRooms();
                BookingRoomList = resultbroom.BookingRooms;

                //get all service
                var resultServices = await financeService.GetServices();
                ServiceList = resultServices.Services;


                var bookingViewList = new List<BookingView>();
               
                foreach (var booking in resultbooking.Bookings)
                {
                    var typename = RoomTypeList.SingleOrDefault(r => r.TypeId == booking.TypeId);
                    var guestname = GuestList.SingleOrDefault(g => g.GuestId == booking.GuestId);
                    var bookingView = new BookingView
                    {
                        BookingId = booking.BookingId,
                        TypeId = booking.TypeId,
                        TypeName = typename.Name,
                        GuestId = booking.GuestId,
                        GuestFirstName = guestname.FirstName,
                        GuestLastName = guestname.LastName,
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
                    if(roomnumber != null)
                    {
                        bookingView.RoomNumber = roomnumber.Number;
                    }

                    bookingViewList.Add(bookingView);
                }
                BookingList = bookingViewList;
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostConfirmBookingAsync(string BookingId, string RoomId)
        {
            try
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
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }

            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostCheckinBookingAsync(string BookingId)
        {
            try
            {
                Guid bookingIdGuid;
                if (!Guid.TryParse(BookingId, out bookingIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var checkin = new
                {
                    BookingId = bookingIdGuid
                };

                var resultconfirm = await bookingService.UpdateBookingCheckin(checkin);
            }
            catch(ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }


            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostCheckoutBookingAsync(string BookingId)
        {
            try
            {
                Guid bookingIdGuid;
                if (!Guid.TryParse(BookingId, out bookingIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var checkin = new
                {
                    BookingId = bookingIdGuid
                };

                var resultconfirm = await bookingService.UpdateBookingCheckout(checkin);
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }

            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostAddServiceAsync(string BookingId, string ServiceId, int ServiceNumber)
        {
            try
            {
                Guid bookingIdGuid, serviceIdGuid;
                if (!Guid.TryParse(BookingId, out bookingIdGuid) || !Guid.TryParse(ServiceId, out serviceIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var booking = BookingList.SingleOrDefault(b => b.BookingId == bookingIdGuid);

                var objAddService = new
                {
                    BookingId = bookingIdGuid,
                    GuestId = booking.GuestId,
                    ServiceId = serviceIdGuid,
                    Numberofservice = ServiceNumber
                };

                var resultOrdering = await financeService.CreateOrdering(objAddService);

            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }



            return RedirectToPage("Booking");
        }
    }
}
