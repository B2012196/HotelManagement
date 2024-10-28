namespace FinanceManagement.API.Features.Services.DeleteService
{
    public record DeleteServiceResponse(bool IsSuccess);
    public class DeleteServiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/finance/services/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteServiceCommand(id));

                var response = result.Adapt<DeleteServiceResponse>();  

                return Results.Ok(response);
            })
            .WithName("DeleteService")
            .Produces<DeleteServiceResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Service")
            .WithDescription("Delete Service");
        }
    }
}
