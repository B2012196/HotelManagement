namespace FinanceManagement.API.Features.Orderings.DeleteOrdering
{
    public record DeleteOrderingResponse(bool IsSuccess);
    public class DeleteOrderingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/finance/orderings/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOrderingCommand(id));

                var response = result.Adapt<DeleteOrderingResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteOrdering")
            .Produces<DeleteOrderingResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Ordering")
            .WithDescription("Delete Ordering");
        }
    }
}
