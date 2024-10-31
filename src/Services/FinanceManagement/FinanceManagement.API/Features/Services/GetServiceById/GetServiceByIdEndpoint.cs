namespace FinanceManagement.API.Features.Services.GetServiceById
{
    public record GetServiceByIdResponse(Service Service);
    public class GetServiceByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/services/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetServiceByIdQuery(id));

                var response = result.Adapt<GetServiceByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetServiceById")
            .Produces<GetServiceByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Service By Id")
            .WithDescription("Get Service By Id");
        }
    }
}
