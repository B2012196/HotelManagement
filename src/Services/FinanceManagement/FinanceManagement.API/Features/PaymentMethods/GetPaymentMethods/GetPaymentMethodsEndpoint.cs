namespace FinanceManagement.API.Features.PaymentMethods.GetPaymentMethods
{
    public record GetPaymentMethodsResponse(IEnumerable<PaymentMethod> PaymentMethods);
    public class GetPaymentMethodsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/paymethods", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPaymentMethodsQuery());

                var response = result.Adapt<GetPaymentMethodsResponse>();  
                
                return Results.Ok(response);
            })
            .WithName("GetPaymentMethods")
            .Produces<GetPaymentMethodsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get PaymentMethods")
            .WithDescription("Get PaymentMethods");
        }
    }
}
