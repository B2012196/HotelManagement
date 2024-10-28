namespace FinanceManagement.API.Features.PaymentMethods.CreatePaymentMethod
{
    public record CreatePaymentMethodRequest(string PaymentMethodName);
    public record CreatePaymentMethodResponse(Guid PaymentMethodId);
    public class CreatePaymentMethodEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/paymethods", async (CreatePaymentMethodRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreatePaymentMethodCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreatePaymentMethodResponse>();

                return Results.Created($"/finance/paymethods/{response.PaymentMethodId}", response);
            })
            .WithName("CreatePaymentMethod")
            .Produces<CreatePaymentMethodResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create PaymentMethod")
            .WithDescription("Create PaymentMethod");
        }
    }
}
