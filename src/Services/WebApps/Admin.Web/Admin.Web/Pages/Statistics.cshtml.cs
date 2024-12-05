namespace Admin.Web.Pages
{
    public class StatisticsModel : PageModel
    {
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Statistics = new StatisticsViewModel
            {
                Labels = new List<string> { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 10", "Tháng 11", "Tháng 12" },
                Data = new List<int> { 19, 31, 43, 40, 51, 21, 11, 25, 45, 86, 62, 33 }
            };

            return Page();
        }
    }
}
