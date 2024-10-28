namespace FinanceManagement.API.Features.Orderings.CreateOrdering
{
    public record CreateOrderingCommand(Guid BookingId, Guid GuestId) : ICommand<CreateOrderingResult>;
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

            return new CreateOrderingResult(ordering.OrderingId);
        }
    }
}
