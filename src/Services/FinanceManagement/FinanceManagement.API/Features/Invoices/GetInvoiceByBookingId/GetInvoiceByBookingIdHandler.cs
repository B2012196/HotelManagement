
namespace FinanceManagement.API.Features.Invoices.GetInvoiceByBookingId
{
    public record GetInvoiceByBookingIdQuery(Guid BookingId) : IQuery<GetInvoiceByBookingIdResult>;
    public record GetInvoiceByBookingIdResult(Invoice Invoice);
    public class GetInvoiceByBookingIdHandler(ApplicationDbContext context)
        : IQueryHandler<GetInvoiceByBookingIdQuery, GetInvoiceByBookingIdResult>
    {
        public async Task<GetInvoiceByBookingIdResult> Handle(GetInvoiceByBookingIdQuery query, CancellationToken cancellationToken)
        {
            var invoice = await context.Invoices.SingleOrDefaultAsync(i => i.BookingId == query.BookingId, cancellationToken);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException(query.BookingId);
            }

            return new GetInvoiceByBookingIdResult(invoice);
        }
    }
}
