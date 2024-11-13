namespace FinanceManagement.API.Features.Statistics.Revenue_Statistics
{
    public record RevenueStatisticsQuery(DateTime Date, string StatisticsType) : IQuery<RevenueStatisticsResult>;
    public record RevenueStatisticsResult(RevenueStatistics RevenueStatistics);
    public record RevenueStatistics(decimal TotalRevenue, int TotalBookings);
    public class StatisticsDayHandler(ApplicationDbContext context)
        : IQueryHandler<RevenueStatisticsQuery, RevenueStatisticsResult>
    {
        public async Task<RevenueStatisticsResult> Handle(RevenueStatisticsQuery query, CancellationToken cancellationToken)
        {
            decimal totalRevenue = 0;
            int totalBookings = 0;
            switch (query.StatisticsType)
            {
                case "Day":
                    var invoice = await context.Invoices
                        .Where(i => i.CreateAt.Date == query.Date.Date)
                        .ToListAsync(cancellationToken);
                    // Tính tổng doanh thu
                    totalRevenue = invoice.Sum(i => i.TotalPrice);
                    // Tính tổng số lượng đặt phòng
                    totalBookings = invoice.Count;
                    break;

                case "Month":
                    // Lọc hóa đơn theo tháng và năm của ngày trong query
                    var monthlyInvoiceData = await context.Invoices
                        .Where(i => i.CreateAt.Year == query.Date.Year && i.CreateAt.Month == query.Date.Month)
                        .ToListAsync(cancellationToken);

                    // Tính toán doanh thu và số lượng đặt phòng
                    totalRevenue = monthlyInvoiceData.Sum(i => i.TotalPrice);
                    totalBookings = monthlyInvoiceData.Count;
                    break;

                case "Year":
                    // Lọc hóa đơn theo năm của ngày trong query
                    var yearlyInvoiceData = await context.Invoices
                        .Where(i => i.CreateAt.Year == query.Date.Year)
                        .ToListAsync(cancellationToken);

                    // Tính toán doanh thu và số lượng đặt phòng
                    totalRevenue = yearlyInvoiceData.Sum(i => i.TotalPrice);
                    totalBookings = yearlyInvoiceData.Count;
                    break;
                default:
                    throw new ArgumentException("Invalid StatisticsType", nameof(query.StatisticsType));
            }

            // Trả về kết quả thống kê
            var revenueStatistics = new RevenueStatistics(totalRevenue, totalBookings);
            return new RevenueStatisticsResult(revenueStatistics);
        }
    }
}
