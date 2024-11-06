
namespace FinanceManagement.API.Features.InvoiceDetails.CreateInvoiceDetail
{
    public record CreateInvoiceDetailCommand(Guid InvoiceId, Guid ServiceId, int Numberofservice) : ICommand<CreateInvoiceDetailResult>;
    public record CreateInvoiceDetailResult(bool IsSuccess);
    public class CreateInvoiceDetailHandler(ApplicationDbContext context)
        : ICommandHandler<CreateInvoiceDetailCommand, CreateInvoiceDetailResult>
    {
        public async Task<CreateInvoiceDetailResult> Handle(CreateInvoiceDetailCommand command, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == command.ServiceId, cancellationToken);
            if (service == null)
            {
                throw new ServiceNotFoundException(command.ServiceId);
            }
            var invoiceDetail = new InvoiceDetail
            {
                InvoiceId = command.InvoiceId,
                ServiceId = command.ServiceId,
                Numberofservice = command.Numberofservice,
                TotalPrice = command.Numberofservice * service.ServicePrice
            };

            var invoice = await context.Invoices.SingleOrDefaultAsync(i => i.InvoiceId == command.InvoiceId, cancellationToken);
            if (invoice == null)
            {
                throw new InvoiceNotFoundException(command.InvoiceId);
            }

            invoice.TotalPrice += invoiceDetail.TotalPrice;

            context.InvoiceDetails.Add(invoiceDetail);
            context.Invoices.Update(invoice);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateInvoiceDetailResult(true);
        }
    }
}
