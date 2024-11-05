namespace FinanceManagement.API.Features.OrderingDetails.Repositories
{
    public interface IOrdDetailsReRepository
    {
        Task<IEnumerable<InvoiceDetail>> GetInvoiceDetails(CancellationToken cancellationToken);
        Task<InvoiceDetail> GetInvoiceDetailById(Guid InvoiceId, CancellationToken cancellationToken);
        Task<Guid> CreateInvoiceDetail(InvoiceDetail InvoiceDetail, CancellationToken cancellationToken);
        Task<bool> DeleteInvoiceDetail(Guid InvoiceId, CancellationToken cancellationToken);
    }
}
