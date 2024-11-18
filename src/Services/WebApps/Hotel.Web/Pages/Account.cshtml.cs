namespace Hotel.Web.Pages
{
    public class AccountModel(IGuestService guestService, IAuthentication authentication,ILogger<AccountModel> logger) : PageModel
    {
        public Guest Guest { get; set; } = new Guest();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var token = HttpContext.Session.GetString("AccessToken");
                if (token != null)
                {
                    string userId = GetUserIdFromToken(token);
                    if (userId != null)
                    {
                        var resultstaff = await guestService.GetGuestByUserId(Guid.Parse(userId));
                        Guest = resultstaff.Guest;
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

        public async Task<IActionResult> OnPostUpdateGuestAsync(string GuestId, string LastName, string FirstName, DateTime DateofBirst, string Address)
        {
            try
            {
                Guid guestIdGuid;
                if (!Guid.TryParse(GuestId, out guestIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var guest = new Guest
                {
                    GuestId = guestIdGuid,
                    LastName = LastName,
                    FirstName = FirstName,
                    DateofBirst = DateofBirst,
                    Address = Address
                };
                Console.WriteLine(guest.DateofBirst);
                var resultUpdateGuest = await guestService.UpdateGuest(guest);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Account");

        }

        public async Task<IActionResult> OnPostUpdatePasswordAsync(string password, string newPassword, string newPasswordAgain)
        {
            try
            {
                if (newPassword != newPasswordAgain)
                {
                    logger.LogInformation("Error: New password and re-entered password are not the same");
                }
                else
                {
                    var token = HttpContext.Session.GetString("AccessToken");
                    if (token != null)
                    {
                        string userId = GetUserIdFromToken(token);
                        if (userId != null)
                        {

                            var user = new
                            {
                                UserId = Guid.Parse(userId),
                                Password = password,
                                NewPassword = newPassword
                            };
                            var resultPassword = await authentication.UpdatePassword(user);
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

            return RedirectToPage("Account");
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
