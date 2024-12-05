namespace Admin.Web.Pages
{
    public class StatisticsModel : PageModel
    {
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Statistics = new StatisticsViewModel
            {
                Labels = new List<string> { "Th�ng 1", "Th�ng 2", "Th�ng 3", "Th�ng 4", "Th�ng 5", "Th�ng 6", "Th�ng 7", "Th�ng 8", "Th�ng 10", "Th�ng 11", "Th�ng 12" },
                Data = new List<int> { 19, 31, 43, 40, 51, 21, 11, 25, 45, 86, 62, 33 }
            };

            return Page();
        }
    }
}
