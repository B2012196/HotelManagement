using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;

namespace Admin.Web.Pages
{
    public class AccountModel(IAuthentication authentication, ILogger<AccountModel> logger) : PageModel
    {
        public IEnumerable<UserView> UserViewList { get; set; } = new List<UserView>();
        public IEnumerable<Role> RoleList { get; set; } = new List<Role>();
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var resultusers = await authentication.GetUsers();
                var resultroles = await authentication.GetRoles();
                RoleList = resultroles.Roles;
                List<UserView> userViews = new List<UserView>();
                foreach (var user in resultusers.UserDtos)
                {
                    var rolename = resultroles.Roles.SingleOrDefault(r => r.RoleId == user.RoleId);
                    if (rolename != null)
                    {
                        var userView = new UserView
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            FailedLoginAttempt = user.FailedLoginAttempt,
                            IsActive = user.IsActive,
                            CreateAt = user.CreateAt,
                            RoleId = user.RoleId,
                            RoleName = rolename.RoleName
                        };
                        userViews.Add(userView);
                    }
                }
                UserViewList = userViews;
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad request: " + apiEx.Content);
                    TempData["ErrorApiException"] = "Error: Not Found Content";
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


        public async Task<IActionResult> OnPostAddRoleAsync(string RoleName)
        {
            try
            {
                var role = new
                {
                    RoleName = RoleName
                };
                var resultrole = await authentication.CreateRole(role);
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
            return RedirectToPage("Account");

        }
        public async Task<IActionResult> OnPostUpdateRoleAsync(string RoleId, string RoleName)
        {
            try
            {
                Guid roleIdGuid;
                if (!Guid.TryParse(RoleId, out roleIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return RedirectToPage("Account");
                }
                var role = new Role
                {
                    RoleId = roleIdGuid,
                    RoleName = RoleName
                };

                var resultUpdateRole = await authentication.UpdateRole(role);
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
            return RedirectToPage("Account");
        }
        public async Task<IActionResult> OnPostDeleteRoleAsync(string RoleId)
        {
            try
            {
                Guid roleIdGuid;
                if (!Guid.TryParse(RoleId, out roleIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return RedirectToPage("Account");
                }

                var resultDelete = await authentication.DeleteRole(roleIdGuid);
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
            return RedirectToPage("Account");


        }

        public async Task<IActionResult> OnPostAddUserAsync(string RoleId, string UserName, string Email, string PhoneNumber, string Password)
        {
            try
            {
                Guid roleIdGuid;
                if (!Guid.TryParse(RoleId, out roleIdGuid))
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                    logger.LogInformation("Dữ liệu không hợp lệ.");
                    return RedirectToPage("Account");
                }

                var user = new User
                {
                    RoleId = roleIdGuid,
                    UserName = UserName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Password = Password
                };

                var resultCreateUser = await authentication.CreateUser(user);
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
                logger.LogError($"Error: {ex.Message}");
            }
            return RedirectToPage("Account");

        }
    }
}
