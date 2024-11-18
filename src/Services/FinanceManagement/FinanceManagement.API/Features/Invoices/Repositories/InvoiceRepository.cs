
namespace FinanceManagement.API.Features.Invoices.Repositories
{
    public class InvoiceRepository(ApplicationDbContext context) : IInvoiceRepository
    {
        public async Task<bool> UpdateInvoiceTotal(Guid BookingId, decimal TotalPrice, CancellationToken cancellationToken)
        {
            var invoice = await context.Invoices.SingleOrDefaultAsync(i => i.BookingId == BookingId, cancellationToken);
            if (invoice == null)
            {
                throw new InvoiceNotFoundException(BookingId);
            }
            if(invoice.TotalPrice == 0)
            {
                invoice.TotalPrice = TotalPrice;
            }
            else
            {
                invoice.TotalPrice += TotalPrice;
            }
            context.Invoices.Update(invoice);
            await context.SaveChangesAsync(cancellationToken);

            return true;    
        }
    }
}
