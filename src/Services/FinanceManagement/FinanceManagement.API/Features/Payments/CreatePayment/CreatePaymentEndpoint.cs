namespace FinanceManagement.API.Features.Payments.CreatePayment
{
    public record CreatePaymentRequest(Guid InvoiceId, Guid PaymentMethodId);
    public record CreatePaymentResponse(Guid PaymentId);
    public class CreatePaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/payments", async (CreatePaymentRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreatePaymentCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreatePaymentResponse>();

                return Results.Created($"/finance/payments/{response.PaymentId}", response);
            })
            .WithName("CreatePayment")
            .Produces<CreatePaymentResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Payment")
            .WithDescription("Create Payment");
        }
    }
}
