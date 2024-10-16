namespace Admin.Web.Pages
{
    public class IndexModel(IAuthentication authentication) : PageModel
    {
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
                    // Redirect sau khi đăng nhập thành công
                }

            }
            catch(ApiException apiEx)
            {
                if(apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
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
    }
}
