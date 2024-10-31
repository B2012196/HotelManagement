namespace FinanceManagement.API.Features.Services.UpdateService
{
    public record UpdateServiceCommand(Guid ServiceId, string ServiceName, decimal ServicePrice) : ICommand<UpdateServiceResult>;
    public record UpdateServiceResult(bool IsSuccess);
    public class UpdateServiceHandler (ApplicationDbContext context)
        : ICommandHandler<UpdateServiceCommand, UpdateServiceResult>
    {
        public async Task<UpdateServiceResult> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == command.ServiceId, cancellationToken);

            if(service == null)
            {
                throw new ServiceNotFoundException(command.ServiceId);
            }

            service.ServiceName = command.ServiceName;
            service.ServicePrice = command.ServicePrice;

            context.Services.Update(service);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateServiceResult(true);
        }
    }
}
