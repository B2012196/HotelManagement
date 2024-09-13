namespace PaymentManagement.API.Features.PaymentMethods.CreatePaymentMethod
{
    public record CreatePMethodRequest(string Name);
    public record CreatePMethodResponse(Guid PaymentMethodId);
    public class CreatePMethodEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/paymentmethods", async (CreatePMethodRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreatePMethodCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreatePMethodResponse>();

                return Results.Created($"/paymentmethods/{response.PaymentMethodId}", response);
            })
            .WithName("CreatePaymentMethod")
            .Produces<CreatePMethodResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create PaymentMethod")
            .WithDescription("Create PaymentMethod");
        }
    }
}
