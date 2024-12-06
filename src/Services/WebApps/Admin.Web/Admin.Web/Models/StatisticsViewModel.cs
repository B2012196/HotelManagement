namespace Admin.Web.Models
{
    public class StatisticsViewModel
    {
        public List<string> Labels { get; set; }
        public List<decimal> Data { get; set; }
    }

    public record RevenueStatisticsResponse(StatisticsViewModel RevenueStatistics);
}
