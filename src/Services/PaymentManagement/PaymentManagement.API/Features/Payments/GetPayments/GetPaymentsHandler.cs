namespace PaymentManagement.API.Features.Payments.GetPayments
{
    public record GetPaymentsQuery() : IQuery<GetPaymentsResult>;
    public record GetPaymentsResult(IEnumerable<Payment> Payments);
    public class GetPaymentsHandler(ApplicationDbContext context)
        : IQueryHandler<GetPaymentsQuery, GetPaymentsResult>
    {
        public async Task<GetPaymentsResult> Handle(GetPaymentsQuery query, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.ToListAsync(cancellationToken);

            return new GetPaymentsResult(payment);
        }
    }
}
