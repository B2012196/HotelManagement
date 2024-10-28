namespace Hotel.Web.Pages
{
    public class BookingModel(IAuthentication authentication,IGuestService guestService, IBookingService bookingService, ILogger<IndexModel> logger) : PageModel
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
        public UserDto User { get; set; } = new UserDto();
        public Guest Guest { get; set; } = new Guest();

        public async Task<IActionResult> OnGetAsync()
        {
            string roomName = string.Empty;
            Console.WriteLine(RoomType);
            switch (RoomType)
            {
                case "1b0b63e1-96de-4ca8-8fc2-82a319bb896a":
                    roomName = "Phòng Deluxe King";
                    break;
                case "54b8d4d6-c9c6-4cbb-85eb-2f41d516cedf":
                    roomName = "Phòng Deluxe Twin";
                    break;
                case "da78d03e-7c63-43eb-847c-8683c2e6545b":
                    roomName = "Phòng Deluxe Triple";
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

                    if (resultGetGuest != null && resultGetUser != null)
                    {
                        Guest = resultGetGuest.Guest;
                        User = resultGetUser.User;
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBookingAsync()
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

                                if (resultbooking != null)
                                {
                                    // Xử lý khi đặt phòng thành công
                                    Console.WriteLine("Booking successfully created with ID: " + resultbooking.BookingId);
                                }
                                else
                                {
                                    // Xử lý khi có lỗi xảy ra
                                    Console.WriteLine("Error creating booking");
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
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                return RedirectToPage("/Index");
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
