using Microsoft.AspNetCore.Authentication;
namespace Hotel.Web.Pages
{
    public class IndexModel
        (IHotelService hotelService, IAuthentication authentication, IGuestService guestService,
        IBookingService bookingService, ILogger<IndexModel> logger)
        : PageModel
    {
        public IEnumerable<HotelModel> HotelList { get; set; } = new List<HotelModel>();
        public IEnumerable<RoomType> RoomTypeList { get; set; } = new List<RoomType>();
        public async Task<IActionResult> OnGetAsync()
        {
            // Kiểm tra xem token có tồn tại trong session không
            bool isLoggedIn = HttpContext.Session.GetString("AccessToken") != null;
            // Lưu trạng thái vào ViewData để sử dụng trong Razor
            ViewData["IsLoggedIn"] = isLoggedIn;

            logger.LogInformation("Index page visited");
            var result = await hotelService.GetRoomTypes();
            RoomTypeList = result.RoomTypes;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string usernameLogin, string passwordLogin)
        {
            try
            {
                var loginData = new Login { UserName = usernameLogin, Password = passwordLogin };
                var response = await authentication.Login(loginData);

                if (response.IsSuccess)
                {
                    var token = response.Token.Access_token;
                    // Lưu trữ token vào session
                    HttpContext.Session.SetString("AccessToken", token);

                    var storedToken = HttpContext.Session.GetString("AccessToken");
                    if (string.IsNullOrEmpty(storedToken))
                    {
                        Console.WriteLine("Token chưa được lưu vào session");
                    }
                    else
                    {
                        Console.WriteLine("Token đã được lưu vào session: " + storedToken);
                    }
                }

            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorMessage"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorMessage"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác nếu có
                Console.WriteLine($"An error occurred: {ex.Message}");
                TempData["ErrorMessage"] = "Lỗi hệ thống";
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            HttpContext.Session.Remove("AccessToken");
            var storedToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(storedToken))
            {
                Console.WriteLine("Token chưa được lưu vào session");
            }
            else
            {
                Console.WriteLine("Token đã được lưu vào session: " + storedToken);
            }
            // Chuyển hướng về trang đăng nhập hoặc trang chính sau khi đăng xuất
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostRegisterAsync(string username, string email, string phone, string password, string confirmPassword)
        {

            var registerUser = new UserModel
            {
                RoleId = Guid.Parse("7b7aef39-b16f-415e-bbba-3b8a77bef68d"),
                UserName = username,
                Email = email,
                PhoneNumber = phone,
                Password = password,
            };

            var response = await authentication.CreateUser(registerUser);

            if (response != null)
            {
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError("", "error register");
            return Page();
        }



    }
}
