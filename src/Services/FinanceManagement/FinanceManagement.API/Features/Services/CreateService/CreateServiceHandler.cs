namespace FinanceManagement.API.Features.Services.CreateService
{
    public record CreateServiceCommand(string ServiceName) : ICommand<CreateServiceResult>;
    public record CreateServiceResult(Guid ServiceId);
    public class CreateServiceHandler(ApplicationDbContext context)
        : ICommandHandler<CreateServiceCommand, CreateServiceResult>
    {
        public async Task<CreateServiceResult> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
        {
            var service = new Service
            {
                ServiceId = Guid.NewGuid(),
                ServiceName = command.ServiceName,
            };

            context.Services.Add(service);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateServiceResult(service.ServiceId);

        }
    }
}
