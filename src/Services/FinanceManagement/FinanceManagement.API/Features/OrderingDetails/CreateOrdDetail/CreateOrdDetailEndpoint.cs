
namespace FinanceManagement.API.Features.OrderingDetails.CreateOrdDetail
{
    public record CreateOrdDetailRequest(Guid OrderingId, Guid ServiceId, int Numberofservice);
    public record CreateOrdDetailResponse(bool IsSuccess);
    public class CreateOrdDetailEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/orderingdetails", async (CreateOrdDetailRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrdDetailCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateOrdDetailResponse>();

                return Results.Ok(response);
            })
            .WithName("CreateOrderingDetail")
            .Produces<CreateOrdDetailResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Ordering Detail")
            .WithDescription("Create Ordering Detail");
        }
    }
}
