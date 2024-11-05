namespace FinanceManagement.API.Features.OrderingDetails.Repositories
{
    public class OrdDetailsReRepository(ApplicationDbContext context)
        : IOrdDetailsReRepository
    {
        public async Task<Guid> CreateInvoiceDetail(InvoiceDetail InvoiceDetail, CancellationToken cancellationToken)
        {
            context.InvoiceDetails.Add(InvoiceDetail);
            await context.SaveChangesAsync();
            return InvoiceDetail.InvoiceId;
        }

        public Task<bool> DeleteInvoiceDetail(Guid InvoiceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceDetail> GetInvoiceDetailById(Guid InvoiceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvoiceDetail>> GetInvoiceDetails(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
