
namespace FinanceManagement.API.Features.Services.DeleteService
{
    public record DeleteServiceCommand(Guid ServiceId) : ICommand<DeleteServiceResult>;
    public record DeleteServiceResult(bool IsSuccess);
    public class DeleteServiceHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteServiceCommand, DeleteServiceResult>
    {
        public async Task<DeleteServiceResult> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == command.ServiceId, cancellationToken);
               
            if (service == null)
            {
                throw new ServiceNotFoundException(command.ServiceId);
            }

            context.Services.Remove(service);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteServiceResult(true);
        }
    }
}
