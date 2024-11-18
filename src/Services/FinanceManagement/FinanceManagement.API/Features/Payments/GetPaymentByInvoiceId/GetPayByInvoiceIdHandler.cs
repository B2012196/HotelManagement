
namespace FinanceManagement.API.Features.Payments.GetPaymentByInvoiceId
{
    public record GetPayByInvoiceIdQuery(Guid InvoiceId) : IQuery<GetPayByInvoiceIdResult>;
    public record GetPayByInvoiceIdResult(Payment Payment);
    public class GetPayByInvoiceIdHandler(ApplicationDbContext context)
        : IQueryHandler<GetPayByInvoiceIdQuery, GetPayByInvoiceIdResult>
    {
        public async Task<GetPayByInvoiceIdResult> Handle(GetPayByInvoiceIdQuery query, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.SingleOrDefaultAsync(p => p.InvoiceId == query.InvoiceId, cancellationToken);
            if (payment == null)
            {
                throw new PaymentNotFoundException(query.InvoiceId);
            }

            return new GetPayByInvoiceIdResult(payment);
        }
    }
}
