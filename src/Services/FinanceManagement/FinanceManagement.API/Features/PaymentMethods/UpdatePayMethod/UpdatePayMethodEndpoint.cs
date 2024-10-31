namespace FinanceManagement.API.Features.PaymentMethods.UpdatePayMethod
{
    public record UpdatePayMethodRequest(Guid PaymentMethodId, string PaymentMethodName);
    public record UpdatePayMethodResponse(bool IsSuccess);
    public class UpdatePayMethodEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/finance/paymethods", async (UpdatePayMethodRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdatePayMethodCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdatePayMethodResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdatePayMethod")
            .Produces<UpdatePayMethodResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update PayMethod")
            .WithDescription("Update PayMethod");
        }
    }
}
