namespace Admin.Web.Pages
{
    public class AccountModel(IAuthentication authentication) : PageModel
    {
        public IEnumerable<UserDto> UserList { get; set; } = new List<UserDto>();
        public async Task<IActionResult> OnGetAsync()
        {
            var resultusers = await authentication.GetUsers();
            UserList = resultusers.UserDtos;
            return Page();
        }
    }
}
