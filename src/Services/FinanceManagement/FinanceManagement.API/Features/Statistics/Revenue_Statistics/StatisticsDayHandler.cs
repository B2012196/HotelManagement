namespace FinanceManagement.API.Features.Statistics.Revenue_Statistics
{
    public record RevenueStatisticsQuery(string StatisticsType) : IQuery<RevenueStatisticsResult>;
    public record RevenueStatisticsResult(RevenueStatistics RevenueStatistics);
    public class StatisticsDayHandler(ApplicationDbContext context)
        : IQueryHandler<RevenueStatisticsQuery, RevenueStatisticsResult>
    {
        public async Task<RevenueStatisticsResult> Handle(RevenueStatisticsQuery query, CancellationToken cancellationToken)
        {
            // Lấy danh sách hóa đơn đã thanh toán
            var invoices = context.Invoices
                .Where(i => i.InvoiceStatus == InvoiceStatus.Paid)
                .ToList();

            // Tạo danh sách Labels (Tháng 1 đến Tháng 12)
            var labels = Enumerable.Range(1, 12).Select(m => $"Tháng {m}").ToList();

            // Group hóa đơn theo tháng
            var data = invoices
                .GroupBy(i => i.CreateAt.Month) // Nhóm theo tháng
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(i => i.TotalPrice)
                })
                .ToList();

            var monthlyData = labels.Select((label, index) =>
            {
                var month = index + 1; // Tháng tương ứng
                var revenue = data.FirstOrDefault(d => d.Month == month)?.TotalRevenue ?? 0;
                return revenue;
            }).ToList();

            var statistic = new RevenueStatistics
            {
                Labels = labels,
                Data = monthlyData,
            };

            return new RevenueStatisticsResult(statistic);


        }
    }
}
