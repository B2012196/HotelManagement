namespace FinanceManagement.API.Features.Invoices.DeleteInvoice
{
    public record DeleteInvoiceCommand(Guid InvoiceId) : ICommand<DeleteInvoiceResult>;
    public record DeleteInvoiceResult(bool IsSuccess, Guid BookingId);
    public class DeleteInvoiceHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteInvoiceCommand, DeleteInvoiceResult>
    {
        public async Task<DeleteInvoiceResult> Handle(DeleteInvoiceCommand command, CancellationToken cancellationToken)
        {
            var invoice = await context.Invoices.SingleOrDefaultAsync(o => o.InvoiceId == command.InvoiceId, cancellationToken);
            if (invoice == null)
            {
                throw new InvoiceNotFoundException(command.InvoiceId);
            }
            Guid bookingId = invoice.BookingId;
            context.Invoices.Remove(invoice);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteInvoiceResult(true, bookingId);

        }
    }
}
