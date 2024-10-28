namespace FinanceManagement.API.Features.Services.GetServices
{
    public record GetServicesResponse(IEnumerable<Service> Services);
    public class GetServicesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/services", async (ISender sender) =>
            {
                var result = await sender.Send(new GetServicesQuery());

                var response = result.Adapt<GetServicesResponse>();

                return Results.Ok(response);    
            })
            .WithName("GetServices")
            .Produces<GetServicesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Services")
            .WithDescription("Get Services");
        }
    }
}
