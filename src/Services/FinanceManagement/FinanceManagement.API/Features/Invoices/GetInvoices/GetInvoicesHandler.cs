namespace FinanceManagement.API.Features.Invoices.GetInvoices
{
    public record GetInvoicesQuery() : IQuery<GetInvoicesResult>;
    public record GetInvoicesResult(IEnumerable<Invoice> Invoices);
    public class GetInvoicesHandler(ApplicationDbContext context)
        : IQueryHandler<GetInvoicesQuery, GetInvoicesResult>
    {
        public async Task<GetInvoicesResult> Handle(GetInvoicesQuery query, CancellationToken cancellationToken)
        {
            var orderings = await context.Invoices.ToListAsync(cancellationToken);

            return new GetInvoicesResult(orderings);
        }
    }
}
