namespace FinanceManagement.API.Features.Orderings.CreateOrdering
{
    public record CreateOrderingRequest(Guid BookingId, Guid GuestId);
    public record CreateOrderingResponse(Guid OrderingId);
    public class CreateOrderingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/orderings", async (CreateOrderingRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderingCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateOrderingResponse>();

                return Results.Created($"/finance/orderings/{response.OrderingId}", response);
            })
            .WithName("CreateOrdering")
            .Produces<CreateOrderingResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Ordering")
            .WithDescription("Create Ordering");
        }
    }
}
