namespace Admin.Web.Pages
{
    public class GuestModel(IGuestService guestService, ILogger<GuestModel> logger) : PageModel
    {
        public IEnumerable<Guest> GuestList { get; set; } = new List<Guest>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultguests = await guestService.GetGuests();
                GuestList = resultguests.Guests;
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
