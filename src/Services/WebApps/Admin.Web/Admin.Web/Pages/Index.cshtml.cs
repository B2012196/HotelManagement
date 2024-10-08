using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Web.Pages
{
    public class IndexModel(IAuthentication authentication) : PageModel
    {

        public async Task<IActionResult> OnPostAsync(string usernameLogin, string passwordLogin)
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
                // Redirect sau khi đăng nhập thành công
                return RedirectToPage("/Index");
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
    }
}
