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
        public async Task<IActionResult> OnGetAsync(string SearchType, string SearchInput, string FilterStatus)
        {
            try
            {
                //get all booking
                var resultbooking = await bookingService.GetBookings();
                logger.LogInformation("get all booking");
                //get all guest
                var resultguest = await guestService.GetGuests();
                logger.LogInformation("get all guest");
                //get all roomtype
                var resultroomtype = await hotelService.GetRoomTypes();
                logger.LogInformation("get all roomtype");
                //get all room
                var resultroom = await hotelService.GetRooms();
                logger.LogInformation("get all room");
                //get all bookingroom
                var resultbroom = await bookingService.GetBookingRooms();
                logger.LogInformation("get all bookingroom");
                //get all service
                var resultServices = await financeService.GetServices();
                logger.LogInformation("get all service");
                if (resultbooking == null || resultguest == null || resultroomtype == null || resultroom == null ||
                    resultbroom == null || resultServices == null)
                {
                    logger.LogInformation("null do get all");
                    TempData["ErrorApiException"] = "null do get all";
                    return Page();
                }
                GuestList = resultguest.Guests;
                RoomTypeList = resultroomtype.RoomTypes;
                RoomList = resultroom.Rooms;
                BookingRoomList = resultbroom.BookingRooms;
                ServiceList = resultServices.Services;

                // Lọc danh sách Booking theo điều kiện tìm kiếm
                IEnumerable<Booking> filteredBookings = resultbooking.Bookings;

                if (!string.IsNullOrEmpty(SearchType) && !string.IsNullOrEmpty(SearchInput))
                {
                    switch (SearchType)
                    {
                        case "bookingId":
                            filteredBookings = filteredBookings.Where(b => b.BookingCode == SearchInput);
                            break;
                        case "guestName":
                            GuestList = GuestList.Where(b => b.FirstName.Contains(SearchInput, StringComparison.OrdinalIgnoreCase));
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(FilterStatus))
                {
                    switch (FilterStatus)
                    {
                        case "pending":
                            filteredBookings = filteredBookings.Where(b => b.BookingStatus == BookingStatus.Pending);
                            break;
                        case "confirmed":
                            filteredBookings = filteredBookings.Where(b => b.BookingStatus == BookingStatus.Confirmed);
                            break;
                        case "checkedin":
                            filteredBookings = filteredBookings.Where(b => b.BookingStatus == BookingStatus.CheckedIn);
                            break;
                        case "checkedout":
                            filteredBookings = filteredBookings.Where(b => b.BookingStatus == BookingStatus.CheckedOut);
                            break;
                        default:
                            filteredBookings = filteredBookings.Where(b => b.BookingStatus == BookingStatus.Canceled);
                            break;
                    }
                }


                var bookingViewList = new List<BookingView>();

                foreach (var booking in filteredBookings)
                {
                    var typename = RoomTypeList.SingleOrDefault(r => r.TypeId == booking.TypeId);
                    var guestname = GuestList.SingleOrDefault(g => g.GuestId == booking.GuestId);
                    if (guestname == null || typename == null)
                    {
                        logger.LogWarning("Guest or Type are null");
                        continue;
                    }

                    var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

                    if (booking.CheckinDate.HasValue && booking.CheckinDate.Value.Kind == DateTimeKind.Utc)
                    {
                        booking.CheckinDate = TimeZoneInfo.ConvertTimeFromUtc(booking.CheckinDate.Value, vietnamTimeZone);
                    }
                    if (booking.CheckoutDate.HasValue && booking.CheckoutDate.Value.Kind == DateTimeKind.Utc)
                    {
                        booking.CheckinDate = TimeZoneInfo.ConvertTimeFromUtc(booking.CheckoutDate.Value, vietnamTimeZone);
                    }

                    var bookingView = new BookingView
                    {
                        BookingId = booking.BookingId,
                        BookingCode = booking.BookingCode,
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

                    var bookingrooms = BookingRoomList.Where(b => b.BookingId == booking.BookingId).ToList();
                    if (booking.BookingStatus != BookingStatus.Pending)
                    {
                        bookingView.RoomNumber = "";
                        foreach (var bookroom in bookingrooms)
                        {
                            var roomnumber = RoomList.SingleOrDefault(r => r.RoomId == bookroom.RoomId);

                            if (roomnumber != null)
                            {
                                bookingView.RoomNumber += roomnumber.Number + " ";
                            }
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
                logger.LogError($"Error: {ex.Message}");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostConfirmBookingAsync(Guid BookingId, List<Guid> RoomIds)
        {
            try
            {
                var confirm = new
                {
                    BookingId = BookingId,
                    RoomIds = RoomIds
                };

                var resultconfirm = await bookingService.UpdateBookingConfirm(confirm);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Booking");
        }

        public async Task<IActionResult> OnPostCheckinBookingAsync(Guid BookingId, Guid GuestId)
        {
            try
            {
                var checkin = new
                {
                    BookingId = BookingId
                };

                var resultconfirm = await bookingService.UpdateBookingCheckin(checkin);

                //kiem tra co invoice chua, neu co khong can tao invoice, dua vao get by BookingId
                var resultGetInvByBook = await financeService.GetInvoiceByBookingId(BookingId);

            }
            catch(ApiException apiEx)
            {
                if(apiEx.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var objCreateInvoice = new
                    {
                        BookingId = BookingId,
                        GuestId = GuestId,
                        IsStatus = false
                    };
                    //create invoice
                    var resultCreateInvoice = await financeService.CreateInvoice(objCreateInvoice);
                }
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
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
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Invoice");
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
                logger.LogError($"Error: {ex.Message}");
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

                //get guestID
                var resultGetGuest = await guestService.GetGuestByUserId(resultGetUser.UserDto.UserId);

                //tạo booking
                var booking = new Booking
                {
                    BookingId = Guid.Empty,
                    GuestId = resultGetGuest.Guest.GuestId,
                    TypeId = roomTypeIdGuid,
                    ExpectedCheckinDate = ExpectedCheckInDate,
                    ExpectedCheckoutDate = ExpectedCheckOutDate,
                    RoomQuantity = RoomQuantity
                };
                 
                var resultCreateBooking = await bookingService.CreateBooking(booking);
                
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
                        //create user and create guest
                        var user = new User
                        {
                            RoleId = roleIdGuid,
                            UserName = Guid.NewGuid().ToString(), 
                            Email = Guid.NewGuid().ToString(),
                            PhoneNumber = PhoneNumber,
                            Password = string.Empty,
                            IsActive = false,
                        };
                        var resultCreateUser = await authentication.CreateUser(user);

                        //get guest and update guest
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
