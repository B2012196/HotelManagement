namespace PaymentManagement.API.Features.PaymentMethods.UpdatePaymentMethod
{
    public record UpdatePMethodRequest(Guid PaymentMethodId, string PaymentMethodName);
    public record UpdatePMethodResponse(bool IsSuccess);
    public class UpdatePMethodEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/paymentmethods", async (UpdatePMethodRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdatePMethodCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdatePMethodResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdatePaymentMethod")
            .Produces<UpdatePMethodResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update PaymentMethod")
            .WithDescription("Update PaymentMethod");
        }
    }
}
