namespace FinanceManagement.API.Features.Invoices.GetInvoices
{
    public record GetInvoicesQuery(int? pageNumber = 1, int? pageSize = 10, InvoiceStatus? filterStatus = InvoiceStatus.None) : IQuery<GetInvoicesResult>;
    public record GetInvoicesResult(IEnumerable<Invoice> Invoices, int TotalCount);
    public class GetInvoicesHandler(ApplicationDbContext context, ILogger<GetInvoicesHandler> logger)
        : IQueryHandler<GetInvoicesQuery, GetInvoicesResult>
    {
        public async Task<GetInvoicesResult> Handle(GetInvoicesQuery query, CancellationToken cancellationToken)
        {
            var invoices = context.Invoices.AsQueryable();

            invoices = invoices.OrderBy(i => i.InvoiceId);

            if (query.filterStatus != InvoiceStatus.None)
            {
                invoices = invoices.Where(i => i.InvoiceStatus == query.filterStatus);
            }
            int TotalCount = await invoices.CountAsync();
            logger.LogInformation("Totalcount: "+ TotalCount);
            if (query.pageNumber.HasValue && query.pageSize.HasValue)
            {
                int skip = (query.pageNumber.Value - 1) * query.pageSize.Value;
                invoices = invoices.Skip(skip).Take(query.pageSize.Value);
            }

            return new GetInvoicesResult(await invoices.ToListAsync(), TotalCount);
        }
    }
}
