namespace FinanceManagement.API.Features.PaymentMethods.DeletePayMethod
{
    public record DeletePayMethodResponse(bool IsSuccess);
    public class DeletePayMethodEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/finance/paymethods/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeletePayMethodCommand(id));

                var response = result.Adapt<DeletePayMethodResponse>();

                return Results.Ok(response);
            })
            .WithName("DeletePaymentMethod")
            .Produces<DeletePayMethodResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete PaymentMethod")
            .WithDescription("Delete PaymentMethod");
        }
    }
}
