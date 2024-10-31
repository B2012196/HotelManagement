namespace FinanceManagement.API.Features.Services.UpdateService
{
    public record UpdateServiceRequest(Guid ServiceId, string ServiceName, decimal ServicePrice);
    public record UpdateServiceResponse(bool IsSuccess);
    public class UpdateServiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/finance/services", async (UpdateServiceRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateServiceCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateServiceResponse>();   

                return Results.Ok(response);
            })
            .WithName("UpdateService")
            .Produces<UpdateServiceResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Service")
            .WithDescription("Update Service");
        }
    }
}
