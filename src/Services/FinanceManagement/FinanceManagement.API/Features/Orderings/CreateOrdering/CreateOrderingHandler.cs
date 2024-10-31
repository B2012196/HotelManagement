namespace FinanceManagement.API.Features.Orderings.CreateOrdering
{
    public record CreateOrderingCommand(Guid BookingId, Guid GuestId, Guid ServiceId, int Numberofservice) : ICommand<CreateOrderingResult>;
    public record CreateOrderingResult(Guid OrderingId);
    public class CreateOrderingHandler(ApplicationDbContext context)
        : ICommandHandler<CreateOrderingCommand, CreateOrderingResult>
    {
        public async Task<CreateOrderingResult> Handle(CreateOrderingCommand command, CancellationToken cancellationToken)
        {
            var ordering = new Ordering
            {
                OrderingId = Guid.NewGuid(),
                BookingId = command.BookingId,
                GuestId = command.GuestId,
                CreateAt = DateTime.Now,
                OrderingStatus = OrderingStatus.Pending,
                TotalPrice = 0,
            };

            context.Orderings.Add(ordering);
            await context.SaveChangesAsync(cancellationToken);

            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == command.ServiceId, cancellationToken);
            if(service == null)
            {
                throw new ServiceNotFoundException(command.ServiceId);
            }

            var orderingDetail = new OrderingDetail
            {
                OrderingId = ordering.OrderingId,
                ServiceId = command.ServiceId,
                Numberofservice = command.Numberofservice,
                TotalPrice = service.ServicePrice * command.Numberofservice
            };

            context.OrderingDetails.Add(orderingDetail);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrderingResult(ordering.OrderingId);
        }
    }
}
