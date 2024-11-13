
namespace FinanceManagement.API.Features.Statistics.Revenue_Statistics
{
    public record RevenueStatisticsResponse(RevenueStatistics RevenueStatistics);
    public class RevenueStatisticsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/statistics/{type}", async (string type, ISender sender) =>
            {
                DateTime Date = DateTime.Now;
                var result = await sender.Send(new RevenueStatisticsQuery(Date, type));

                var response = result.Adapt<RevenueStatisticsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRevenueStatistics")
            .Produces<RevenueStatisticsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Revenue Statistics")
            .WithDescription("Get Revenue Statistics");
        }
    }
}
