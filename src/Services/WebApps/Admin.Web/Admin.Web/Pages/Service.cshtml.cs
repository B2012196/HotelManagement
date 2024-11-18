namespace Admin.Web.Pages
{
    public class ServiceModel(IFinanceService serviceHotelService, ILogger<ServiceModel> logger) : PageModel
    {
        public IEnumerable<Service> ServiceList { get; set; } = new List<Service>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultServices = await serviceHotelService.GetServices();
                ServiceList = resultServices.Services;
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

        public async Task<IActionResult> OnPostAddServiceAsync(string ServiceName, decimal ServicePrice)
        {
            try
            {
                var service = new Service
                {
                    ServiceName = ServiceName,
                    ServicePrice = ServicePrice
                };

                var resultCreateService = await serviceHotelService.CreateService(service);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Service");
        }

        public async Task<IActionResult> OnPostUpdateServiceAsync(string ServiceId, string ServiceName, decimal ServicePrice)
        {
            try
            {
                Guid serviceIdGuid;
                if (!Guid.TryParse(ServiceId, out serviceIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var service = new Service
                {
                    ServiceId = serviceIdGuid,
                    ServiceName = ServiceName,
                    ServicePrice = ServicePrice
                };

                var resultUpdateService = await serviceHotelService.UpdateService(service);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Service");



        }

        public async Task<IActionResult> OnPostDeleteServiceAsync(string ServiceId)
        {
            try
            {
                Guid serviceIdGuid;
                if (!Guid.TryParse(ServiceId, out serviceIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return Page();
                }

                var resultDeleteService = await serviceHotelService.DeleteService(serviceIdGuid);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }

            return RedirectToPage("Service");
        }

        public async Task<IActionResult> OnPostUploadImageServiceAsync(Guid ServiceId, IFormFile Image)
        {
            try
            {
                // Gửi file dưới dạng multipart/form-data
                using var stream = Image.OpenReadStream();
                var streamPart = new StreamPart(stream, Image.FileName, Image.ContentType);
                var resultUploadImage = await serviceHotelService.UploadServiceImage(ServiceId, streamPart);
            }
            catch (ApiException apiEx)
            {
                HandleApiException(apiEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Service");
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
