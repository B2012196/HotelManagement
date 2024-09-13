
namespace PaymentManagement.API.Features.PaymentMethods.GetPaymentMethods
{
    public record GetPMethodsResponse(IEnumerable<PaymentMethod> PaymentMethods);
    public class GetPMethodsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/paymentmethods", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPMethodsQuery());

                var response = result.Adapt<GetPMethodsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetPaymentMethods")
            .Produces<GetPMethodsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get PaymentMethods")
            .WithDescription("Get PaymentMethods");
        }
    }
}
