using Hotel.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


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
            var loginData = new LoginModel { UserName = usernameLogin, Password = passwordLogin };
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
                // Redirect sau khi đăng nhập thành công
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return Page();
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
