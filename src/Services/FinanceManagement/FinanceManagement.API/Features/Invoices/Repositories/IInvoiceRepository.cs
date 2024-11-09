namespace FinanceManagement.API.Features.Invoices.Repositories
{
    public interface IInvoiceRepository
    {
        Task<bool> UpdateInvoiceTotal(Guid BookingId, decimal TotalPrice, CancellationToken cancellationToken);
    }
}
