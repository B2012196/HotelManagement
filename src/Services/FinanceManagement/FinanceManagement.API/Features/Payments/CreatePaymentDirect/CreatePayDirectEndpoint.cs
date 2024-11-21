
namespace FinanceManagement.API.Features.Payments.CreatePaymentDirect
{
    public record CreatePayDirectRequest(Guid InvoiceId, Guid PaymentMethodId, decimal Amount);
    public record CreatePayDirectResponse(Guid PaymentId);
    public class CreatePayDirectEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/finance/paymentdirect", async (CreatePayDirectRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreatePayDirectCommand>();

                var result = await sender.Send(command);    

                var response = result.Adapt<CreatePayDirectResponse>();

                return Results.Created($"/finance/paymentdirect/{response.PaymentId}", response);
            })
            .WithName("CreatePaymentDirect")
            .Produces<CreatePayDirectResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Payment Direct")
            .WithDescription("Create Payment Direct");
        }
    }
}
