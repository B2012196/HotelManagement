namespace FinanceManagement.API.Features.OrderingDetails.GetOrderingDetails
{
    public record GetOrdDetailsResponse(IEnumerable<OrderingDetail> OrderingDetails);
    public class GetOrdDetailsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/orderingdetails", async (ISender sender) =>
            {
                var result = await sender.Send(new GetOrdDetailsQuery());

                var response = result.Adapt<GetOrdDetailsResponse>();

                return Results.Ok(response); 
            })
            .WithName("GetOrderingDetails")
            .Produces<GetOrdDetailsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Ordering Details")
            .WithDescription("Get Ordering Details");
        }
    }
}
