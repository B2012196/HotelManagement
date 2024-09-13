namespace PaymentManagement.API.Features.Payments.DeletePayment
{
    public record DeletePaymentResponse(bool IsSuccess);
    public class DeletePaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/payments/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeletePaymentCommand(id));

                var response = result.Adapt<DeletePaymentResponse>();

                return Results.Ok(response);
            })
            .WithName("DeletePayment")
            .Produces<DeletePaymentResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Payment")
            .WithDescription("Delete Payment");
        }
    }
}
