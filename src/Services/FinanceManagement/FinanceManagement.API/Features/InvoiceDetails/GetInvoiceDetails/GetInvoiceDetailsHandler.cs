namespace FinanceManagement.API.Features.InvoiceDetails.GetInvoiceDetails
{
    public record GetInvoiceDetailsQuery() : IQuery<GetInvoiceDetailsResult>;
    public record GetInvoiceDetailsResult(IEnumerable<InvoiceDetail> InvoiceDetails);
    public class GetInvoiceDetailsHandler(ApplicationDbContext context)
        : IQueryHandler<GetInvoiceDetailsQuery, GetInvoiceDetailsResult>
    {
        public async Task<GetInvoiceDetailsResult> Handle(GetInvoiceDetailsQuery query, CancellationToken cancellationToken)
        {
            var details = await context.InvoiceDetails.ToListAsync(cancellationToken);

            return new GetInvoiceDetailsResult(details);
        }
    }
}
