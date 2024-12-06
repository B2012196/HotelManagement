namespace Admin.Web.Pages
{
    public class StatisticsModel(IFinanceService financeService) : PageModel
    {
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            var resultGetStatistic = await financeService.GetStatistic("revenue");


            Statistics = new StatisticsViewModel
            {
                Labels = resultGetStatistic.RevenueStatistics.Labels,
                Data = resultGetStatistic.RevenueStatistics.Data,
            };

            return Page();
        }
    }
}
