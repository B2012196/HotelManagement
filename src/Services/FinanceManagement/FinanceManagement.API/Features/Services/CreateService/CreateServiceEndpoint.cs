namespace FinanceManagement.API.Features.Services.CreateService
{
    public record CreateServiceRequest(string ServiceName);
    public record CreateServiceResponse(Guid ServiceId);
    public class CreateServiceEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/services", async(CreateServiceRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateServiceCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateServiceResponse>();

                return Results.Created($"/finance/services/{response.ServiceId}", response);
            })
            .WithName("CreateService")
            .Produces<CreateServiceResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Service")
            .WithDescription("Create Service");
        }
    }
}
