namespace Admin.Web.Pages
{
    public class BookingModel(IAuthentication authentication ,IBookingService bookingService, IGuestService guestService, IHotelService hotelService, 
        IFinanceService financeService,
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
                logger.LogWarning("booking");
                //get all guest
                var resultguest = await guestService.GetGuests();
                logger.LogWarning("guest");
                //get all roomtype
                var resultroomtype = await hotelService.GetRoomTypes();
                logger.LogWarning("roomtype");
                //get all room
                var resultroom = await hotelService.GetRooms();
                logger.LogWarning("room");
                //get all bookingroom
                var resultbroom = await bookingService.GetBookingRooms();
                logger.LogWarning("bookingroom");
                //get all service
                var resultServices = await financeService.GetServices();
                logger.LogWarning("service");
                if (resultbooking == null || resultguest == null || resultroomtype == null || resultroom == null ||
                    resultbroom == null || resultServices == null)
                {
                    logger.LogWarning("null do get all");
                    TempData["ErrorApiException"] = "null do get all";
                    return Page();
                }
                GuestList = resultguest.Guests;
                RoomTypeList = resultroomtype.RoomTypes;
                RoomList = resultroom.Rooms;
                BookingRoomList = resultbroom.BookingRooms;
                ServiceList = resultServices.Services;

                var bookingViewList = new List<BookingView>();

                foreach (var booking in resultbooking.Bookings)
                {
                    var typename = RoomTypeList.SingleOrDefault(r => r.TypeId == booking.TypeId);
                    var guestname = GuestList.SingleOrDefault(g => g.GuestId == booking.GuestId);
                    if (guestname == null || typename == null)
                    {
                        logger.LogWarning("Guest or Type are null");
                        return Page();
                    }
                    logger.LogWarning("Guest or Type are not null");
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
                    logger.LogWarning("" + bookingView.TotalPrice);

                    logger.LogWarning(booking.BookingId + "");

                    var bookingroom = BookingRoomList.Where(b => b.BookingId == booking.BookingId).ToList();
                    if (booking.BookingStatus != BookingStatus.Pending)
                    {
                        logger.LogWarning(bookingroom[0].RoomId + "");
                        var roomnumber = RoomList.SingleOrDefault(r => r.RoomId == bookingroom[0].RoomId);

                        if (roomnumber != null)
                        {
                            bookingView.RoomNumber = roomnumber.Number;
                        }
                    }

                    bookingViewList.Add(bookingView);
                }
                BookingList = bookingViewList;
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
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
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }

            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostCheckinBookingAsync(string BookingId, string GuestId)
        {
            try
            {
                Guid bookingIdGuid, guestIdGuid;
                if (!Guid.TryParse(BookingId, out bookingIdGuid) || !Guid.TryParse(GuestId, out guestIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var checkin = new
                {
                    BookingId = bookingIdGuid
                };

                var resultconfirm = await bookingService.UpdateBookingCheckin(checkin);

                var objCreateOrdering = new
                {
                    BookingId = bookingIdGuid,
                    GuestId = guestIdGuid
                };

                var resultCreateOrdering = await financeService.CreateInvoice(objCreateOrdering);

            }
            catch(ApiException apiEx)
            {
                HandleApiException(apiEx);
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
                HandleApiException(apiEx);
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
                //get invoice by bookingId
                var resultGetInvoice = await financeService.GetInvoiceByBookingId(bookingIdGuid);

                var detail = new InvoiceDetail
                {
                    InvoiceId = resultGetInvoice.Invoice.InvoiceId,
                    ServiceId = serviceIdGuid,
                    Numberofservice = ServiceNumber,
                    TotalPrice = 0,
                };

                var resultOrdering = await financeService.CreateInvoiceDetail(detail);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }
            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostAddBookingAsync(string PhoneNumber, string LastName, string FirstName, string Address, DateTime DateofBirth,
            string RoomType, int RoomQuantity, DateTime ExpectedCheckInDate, DateTime ExpectedCheckOutDate)
        {
            try
            {
                Guid roomTypeIdGuid;
                if (!Guid.TryParse(RoomType, out roomTypeIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }
                //if Kiem tra chưa có tài khoản thì tạo
                var resultGetUser = await authentication.GetUserByPhone(PhoneNumber);
                //tim thay user
                if(resultGetUser != null)
                {
                    //tao booking
                }
                
            }
            catch (ApiException apiEx)
            {
                try
                {
                    if (apiEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {

                        Guid roleIdGuid, roomTypeIdGuid;
                        if (!Guid.TryParse("97864e5a-0172-49fe-9c42-abb9e90ad022", out roleIdGuid) || !Guid.TryParse(RoomType, out roomTypeIdGuid))
                        {
                            logger.LogInformation("Dữ liệu không hợp lệ.");
                            return Page();
                        }
                        //create user
                        var user = new User
                        {
                            RoleId = roleIdGuid,
                            UserName = string.Empty,
                            Email = string.Empty,
                            PhoneNumber = PhoneNumber,
                            Password = string.Empty
                        };
                        var resultCreateUser = await authentication.CreateUser(user);

                        //create guest
                        var resultGetGuestByUserId = await guestService.GetGuestByUserId(resultCreateUser.UserId);
                        var guest = new Guest
                        {
                            GuestId = resultGetGuestByUserId.Guest.GuestId,
                            UserId = resultCreateUser.UserId,
                            FirstName = FirstName,
                            LastName = LastName,
                            DateofBirst = DateofBirth,
                            Address = Address
                        };
                        var resultUpdateGuest = await guestService.UpdateGuest(guest);

                        //tạo booking
                        var booking = new Booking
                        {
                            BookingId = Guid.Empty,
                            GuestId = resultGetGuestByUserId.Guest.GuestId,
                            TypeId = roomTypeIdGuid, 
                            ExpectedCheckinDate = ExpectedCheckInDate,
                            ExpectedCheckoutDate = ExpectedCheckOutDate,
                            RoomQuantity = RoomQuantity
                        };

                        var resultCreateBooking = await bookingService.CreateBooking(booking);
                    }
                    else
                    {
                        HandleApiException(apiEx);
                    }
                }
                catch (ApiException apiEx2)
                {
                    HandleApiException(apiEx2);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Booking");
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

    }
}
