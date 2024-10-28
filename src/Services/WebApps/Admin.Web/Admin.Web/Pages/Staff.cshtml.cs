using Microsoft.Extensions.Logging;

namespace Admin.Web.Pages
{
    public class StaffModel(IStaffService staffService, ILogger<StaffModel> logger) : PageModel
    {
        public IEnumerable<Staff> StaffList { get; set; } = new List<Staff>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultstaff = await staffService.GetStaffs();

                StaffList = resultstaff.Staffs;
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không tìm thấy nội dung";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Đăng nhập để tiếp tục";
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Unauthorized: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Không có quyền truy cập";
                }
                else
                {
                    Console.WriteLine($"Error: {apiEx.StatusCode}, Content: {apiEx.Content}");
                    TempData["ErrorApiException"] = "Lỗi hệ thống";
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching guests: {ex.Message}");
            }
            return Page();
        }
    }
}
