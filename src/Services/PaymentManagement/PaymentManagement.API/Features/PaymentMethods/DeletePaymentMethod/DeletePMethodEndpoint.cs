namespace PaymentManagement.API.Features.PaymentMethods.DeletePaymentMethod
{
    public record DeletePMethodResponse(bool IsSuccess);
    public class DeletePMethodEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/paymentmethods/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeletePMethodCommand(id));

                var response = result.Adapt<DeletePMethodResponse>();

                return Results.Ok(response);
            })
            .WithName("DeletePaymentMethod")
            .Produces<DeletePMethodResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete PaymentMethod")
            .WithDescription("Delete PaymentMethod");
        }
    }
}
