

namespace FinanceManagement.API.Features.Invoices.UpdateInvoice
{
    public record UpdateInvoiceCommand(Guid InvoiceId) : ICommand<UpdateInvoiceResult>;
    public record UpdateInvoiceResult(bool IsSuccess);
    public class UpdateInvoiceHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateInvoiceCommand, UpdateInvoiceResult>
    {
        public async Task<UpdateInvoiceResult> Handle(UpdateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var invoice = await context.Invoices.SingleOrDefaultAsync(i => i.InvoiceId == command.InvoiceId, cancellationToken);
            if (invoice == null)
            {
                throw new InvoiceNotFoundException(command.InvoiceId);
            }

            invoice.InvoiceStatus = InvoiceStatus.Paid;

            context.Invoices.Update(invoice);

            await context.SaveChangesAsync(cancellationToken);

            return new UpdateInvoiceResult(true);
        }
    }
}
