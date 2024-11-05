
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

            context.InvoiceDetails.Add(invoiceDetail);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateInvoiceDetailResult(true);
        }
    }
}
