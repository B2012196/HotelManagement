namespace Admin.Web.Pages.Shared
{
    public class StatisticsModel : PageModel
    {
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Statistics = new StatisticsViewModel
            {
                Labels = new List<string> { "January", "February", "March", "April" },
                Data = new List<int> { 10, 20, 30, 40 }
            };

            return Page();
        }
    }
}
