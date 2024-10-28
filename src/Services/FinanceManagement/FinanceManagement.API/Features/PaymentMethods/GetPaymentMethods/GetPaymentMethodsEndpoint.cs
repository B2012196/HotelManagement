
namespace FinanceManagement.API.Features.PaymentMethods.GetPaymentMethods
{
    public record GetPaymentMethodsResponse(IEnumerable<PaymentMethod> PaymentMethods);
    public class GetPaymentMethodsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/finance/paymethods", async (ISender sender) =>
            {
                //var result = 
            });
        }
    }
}
