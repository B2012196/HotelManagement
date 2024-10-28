
namespace FinanceManagement.API.Features.PaymentMethods.GetPaymentMethods
{
    public record GetPaymentMethodsQuery() : IQuery<GetPaymentMethodsResult>;
    public record GetPaymentMethodsResult(IEnumerable<PaymentMethod> PaymentMethods);
    public class GetPaymentMethodsHandler(ApplicationDbContext context)
        : IQueryHandler<GetPaymentMethodsQuery, GetPaymentMethodsResult>
    {
        public async Task<GetPaymentMethodsResult> Handle(GetPaymentMethodsQuery query, CancellationToken cancellationToken)
        {
            var paymethod = await context.PaymentMethods.ToListAsync(cancellationToken);

            return new GetPaymentMethodsResult(paymethod);
        }
    }
}
