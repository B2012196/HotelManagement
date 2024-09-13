
namespace PaymentManagement.API.Features.PaymentMethods.GetPaymentMethods
{
    public record GetPMethodsQuery() : IQuery<GetPMethodsResult>;
    public record GetPMethodsResult(IEnumerable<PaymentMethod> PaymentMethods);
    public class GetPMethodsHandler(ApplicationDbContext context)
        : IQueryHandler<GetPMethodsQuery, GetPMethodsResult>
    {
        public async Task<GetPMethodsResult> Handle(GetPMethodsQuery query, CancellationToken cancellationToken)
        {
            var methods = await context.PaymentMethods.ToListAsync(cancellationToken);

            return new GetPMethodsResult(methods);
        }
    }
}
