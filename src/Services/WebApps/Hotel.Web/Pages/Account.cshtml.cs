namespace Hotel.Web.Pages
{
    public class AccountModel(IGuestService guestService, IAuthentication authentication,ILogger<AccountModel> logger) : PageModel
    {
        public Guest Guest { get; set; } = new Guest();
        public async Task<IActionResult> OnGetAsync()
        {

            var token = HttpContext.Session.GetString("AccessToken");
            if (token != null)
            {
                string userId = GetUserIdFromToken(token);
                if (userId != null)
                {
                    var resultstaff = await guestService.GetGuestByUserId(Guid.Parse(userId));
                    if (resultstaff != null)
                    {
                        Guest = resultstaff.Guest;
                    }
                }
                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateGuestAsync(string GuestId, string LastName, string FirstName, DateTime DateofBirst, string Address)
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
            if (!resultUpdateGuest.IsSuccess)
            {
                logger.LogInformation("Error: Cannot update the guest");
            }

            return RedirectToPage("Account");

        }

        public async Task<IActionResult> OnPostUpdatePasswordAsync(string password, string newPassword, string newPasswordAgain)
        {
            if(newPassword != newPasswordAgain)
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

                        if (!resultPassword.IsSuccess)
                        {
                            logger.LogInformation("Error: Cannot update the password");
                        }
                    }
                }
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
    }
}
