namespace Admin.Web.Pages.Shared.Components.Auth
{
    public class AuthViewComponent(IHttpContextAccessor httpContextAccessor, IAuthentication authentication) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("AccessToken");

            var model = new AuthViewModel
            {
                IsAuthenticated = !string.IsNullOrEmpty(token),
            };

            return View(model);
        }
    }
    public class AuthViewModel
    {
        public bool IsAuthenticated { get; set; }
    }
}
