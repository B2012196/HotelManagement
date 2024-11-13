namespace Hotel.Web.Pages
{
    public class BookingModel(IFinanceService financeService ,IHotelService hotelService, IAuthentication authentication,IGuestService guestService, IBookingService bookingService, ILogger<IndexModel> logger) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string RoomType { get; set; }
        public string RoomTypeConfirm { get; set; }
        public string RoomTypeName { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime ExpectedCheckInDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime ExpectedCheckOutDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public int RoomQuantity { get; set; }
        public decimal Price { get; set; }
        public UserDto User { get; set; } = new UserDto();
        public Guest Guest { get; set; } = new Guest();
        public RoomType RoomTypeModel  { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                string roomName = string.Empty;
                string RoomTypeId = string.Empty;
                Console.WriteLine(RoomType);
                switch (RoomType)
                {
                    case "1b0b63e1-96de-4ca8-8fc2-82a319bb896a":
                        roomName = "Phòng Deluxe King";
                        RoomTypeId = "1b0b63e1-96de-4ca8-8fc2-82a319bb896a";
                        break;
                    case "54b8d4d6-c9c6-4cbb-85eb-2f41d516cedf":
                        roomName = "Phòng Deluxe Twin";
                        RoomTypeId = "54b8d4d6-c9c6-4cbb-85eb-2f41d516cedf";
                        break;
                    case "da78d03e-7c63-43eb-847c-8683c2e6545b":
                        roomName = "Phòng Deluxe Triple";
                        RoomTypeId = "da78d03e-7c63-43eb-847c-8683c2e6545b";
                        break;
                    default:
                        roomName = "Unknown Room";
                        break;
                }

                RoomTypeName = roomName;

                var token = HttpContext.Session.GetString("AccessToken");
                if (token != null)
                {
                    string userId = GetUserIdFromToken(token);
                    if (userId != null)
                    {
                        Console.WriteLine($"User ID: {userId}");
                        //get guest
                        var resultGetGuest = await guestService.GetGuestByUserId(Guid.Parse(userId));
                        //get user
                        var resultGetUser = await authentication.GetUserByUserId(Guid.Parse(userId));
                        //get roomtype 
                        var resultGetRoomType = await hotelService.GetRoomTypeById(Guid.Parse(RoomTypeId));

                        if (resultGetGuest != null && resultGetUser != null)
                        {
                            Guest = resultGetGuest.Guest;
                            User = resultGetUser.User;
                        }

                        RoomTypeModel = resultGetRoomType.RoomType;

                        int totalDays = (ExpectedCheckOutDate - ExpectedCheckInDate).Days;
                        Price = totalDays * RoomTypeModel.PricePerNight * RoomQuantity;
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

        public async Task<IActionResult> OnPostBookingAsync(decimal RoomTypePrice, DateTime ExpectedCheckInDate, DateTime ExpectedCheckOutDate)
        {
            try
            {
                switch (RoomType)
                {
                    case "Phòng Deluxe King":
                        RoomTypeConfirm = "1b0b63e1-96de-4ca8-8fc2-82a319bb896a";
                        break;
                    case "Phòng Deluxe Twin":
                        RoomTypeConfirm = "54b8d4d6-c9c6-4cbb-85eb-2f41d516cedf";
                        break;
                    case "Phòng Deluxe Triple":
                        RoomTypeConfirm = "da78d03e-7c63-43eb-847c-8683c2e6545b";
                        break;
                    default:
                        RoomTypeConfirm = "Unknown Room";
                        break;
                }

                var token = HttpContext.Session.GetString("AccessToken");
                if (token != null)
                {
                    string userId = GetUserIdFromToken(token);
                    if (userId != null)
                    {
                        Console.WriteLine($"User ID: {userId}");
                        //get guest
                        var resultGetGuest = await guestService.GetGuestByUserId(Guid.Parse(userId));
                        //get user
                        var resultGetUser = await authentication.GetUserByUserId(Guid.Parse(userId));
                        
                        if (resultGetGuest != null && resultGetUser != null)
                        {
                            Guest = resultGetGuest.Guest;
                            User = resultGetUser.User;
                            // Kiểm tra RoomTypeConfirm có giá trị hay không trước khi parse
                            if (!string.IsNullOrEmpty(RoomTypeConfirm))
                            {
                                Console.WriteLine("RoomType not null: " + RoomTypeConfirm);
                                var booking = new Booking
                                {
                                    BookingId = Guid.Empty,
                                    GuestId = resultGetGuest.Guest.GuestId,
                                    TypeId = Guid.Parse(RoomTypeConfirm), // Chỉ parse nếu RoomTypeConfirm không null
                                    ExpectedCheckinDate = ExpectedCheckInDate,
                                    ExpectedCheckoutDate = ExpectedCheckOutDate,
                                    RoomQuantity = RoomQuantity
                                };

                                Console.WriteLine(booking);
                                var resultbooking = await bookingService.CreateBooking(booking);
                                if(resultbooking.BookingId != Guid.Empty)
                                {
                                    Console.WriteLine("BookingId: " +resultbooking.BookingId);
                                    //create invoice
                                    var objCreateInvoice = new
                                    {
                                        BookingId = resultbooking.BookingId,    
                                        GuestId = resultGetGuest.Guest.GuestId,
                                        IsStatus = true,
                                    };

                                    var resultCreateInvoice = await financeService.CreateInvoice(objCreateInvoice);
                                    //pay invoice
                                    if (resultCreateInvoice.InvoiceId != Guid.Empty)
                                    {
                                        Guid paymentMethodId = Guid.Parse("ed82b3a3-69ec-475e-961f-ba1a854d0348");
                                        int totalDays = (ExpectedCheckOutDate - ExpectedCheckInDate).Days;
                                        var objPayInvoice = new
                                        {
                                            InvoiceId = resultCreateInvoice.InvoiceId,
                                            PaymentMethodId = paymentMethodId,
                                            FullName = resultGetGuest.Guest.LastName + " " + resultGetGuest.Guest.FirstName,
                                            Price = totalDays * RoomTypePrice * RoomQuantity,
                                        };
                                        var resultCreatePayment = await financeService.CreatePayment(objPayInvoice);
                                        return Redirect(resultCreatePayment.PaymentUrl);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("RoomTypeConfirm is null or empty");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Result null: " + resultGetGuest);
                        }
                    }
                    else
                    {
                        Console.WriteLine("User ID not found in token.");
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
            return RedirectToPage("Index");
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
