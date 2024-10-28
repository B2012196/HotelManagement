namespace FinanceManagement.API.Features.Orderings.GetOrderings
{
    public record GetOrderingsResponse(IEnumerable<Ordering> Orderings);
    public class GetOrderingsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/orderings", async(ISender sender) =>
            {
                var result = await sender.Send(new GetOrderingsQuery());

                var response = result.Adapt<GetOrderingsResponse>();    

                return Results.Ok(response);
            })
            .WithName("GetOrderings")
            .Produces<GetOrderingsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orderings")
            .WithDescription("Get Orderings");
        }
    }
}
