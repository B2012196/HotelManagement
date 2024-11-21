using System.Net;
using System.Numerics;

namespace Admin.Web.Pages
{
    public class GuestModel(IGuestService guestService, IAuthentication authentication, ILogger<GuestModel> logger) : PageModel
    {
        public IEnumerable<GuestView> GuestList { get; set; } = new List<GuestView>();
        public IEnumerable<UserDto> UserList { get; set; } = new List<UserDto>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultguests = await guestService.GetGuests();
                var resultusers = await authentication.GetUsers();

                UserList = resultusers.UserDtos;

                List<GuestView>  guestViews = new List<GuestView>(); 
                foreach (var guest in resultguests.Guests)
                {
                    var user = UserList.SingleOrDefault(u => u.UserId == guest.UserId);

                    var guestView = new GuestView
                    {
                        GuestId = guest.GuestId,
                        UserId = guest.UserId,
                        FirstName = guest.FirstName,
                        LastName = guest.LastName,
                        DateofBirst = guest.DateofBirst,
                        Address = guest.Address,
                        Phone = user.PhoneNumber,
                        Email = user.Email,
                        IsActive = user.IsActive,
                    };

                    guestViews.Add(guestView);
                }
                GuestList = guestViews;
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

        public async Task<IActionResult> OnPostDeleteStaffAsync(string GuestId)
        {
            try
            {
                Guid guestIdGuid;
                if (!Guid.TryParse(GuestId, out guestIdGuid))
                {
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var resultDeleteRoom = await guestService.DeleteGuest(guestIdGuid);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Guest");
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
