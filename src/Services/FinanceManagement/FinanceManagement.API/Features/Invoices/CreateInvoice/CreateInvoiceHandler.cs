namespace FinanceManagement.API.Features.Invoices.CreateInvoice
{
    public record CreateInvoiceCommand(Guid BookingId, Guid GuestId, bool IsStatus) : ICommand<CreateInvoiceResult>;
    public record CreateInvoiceResult(Guid InvoiceId);
    public class CreateInvoiceHandler(ApplicationDbContext context)
        : ICommandHandler<CreateInvoiceCommand, CreateInvoiceResult>
    {
        public async Task<CreateInvoiceResult> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var invoice = new Invoice
            {
                InvoiceId = Guid.NewGuid(),
                BookingId = command.BookingId,
                GuestId = command.GuestId,
                CreateAt = DateTime.UtcNow,
                TotalPrice = 0,
            };
            if(command.IsStatus)
            {
                invoice.InvoiceStatus = InvoiceStatus.PartiallyPaid;
            }
            else
            {
                invoice.InvoiceStatus = InvoiceStatus.Pending;
            }

            context.Invoices.Add(invoice);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateInvoiceResult(invoice.InvoiceId);
        }
    }
}
