namespace FinanceManagement.API.Features.Invoices.CreateInvoice
{
    public record CreateInvoiceCommand(Guid BookingId, Guid GuestId) : ICommand<CreateInvoiceResult>;
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
                CreateAt = DateTime.Now,
                InvoiceStatus = InvoiceStatus.Pending,
                TotalPrice = 0,
            };

            context.Invoices.Add(invoice);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateInvoiceResult(invoice.InvoiceId);
        }
    }
}
