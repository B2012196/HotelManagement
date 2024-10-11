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
            catch (Exception ex)
            {
                logger.LogInformation($"{ex.Message}");
            }
            return Page();
        }
    }
}
