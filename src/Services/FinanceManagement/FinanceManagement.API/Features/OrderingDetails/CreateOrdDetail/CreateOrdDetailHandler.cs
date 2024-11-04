
namespace FinanceManagement.API.Features.OrderingDetails.CreateOrdDetail
{
    public record CreateOrdDetailCommand(Guid OrderingId, Guid ServiceId, int Numberofservice) : ICommand<CreateOrdDetailResult>;
    public record CreateOrdDetailResult(bool IsSuccess);
    public class CreateOrdDetailHandler(ApplicationDbContext context)
        : ICommandHandler<CreateOrdDetailCommand, CreateOrdDetailResult>
    {
        public async Task<CreateOrdDetailResult> Handle(CreateOrdDetailCommand command, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == command.ServiceId, cancellationToken);
            if (service == null)
            {
                throw new ServiceNotFoundException(command.ServiceId);
            }
            var ordDetail = new OrderingDetail
            {
                OrderingId = command.OrderingId,
                ServiceId = command.ServiceId,
                Numberofservice = command.Numberofservice,
                TotalPrice = command.Numberofservice * service.ServicePrice
            };

            context.OrderingDetails.Add(ordDetail);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrdDetailResult(true);
        }
    }
}
