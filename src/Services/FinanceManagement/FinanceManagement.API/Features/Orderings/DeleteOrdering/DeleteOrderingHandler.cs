
namespace FinanceManagement.API.Features.Orderings.DeleteOrdering
{
    public record DeleteOrderingCommand(Guid OrderingId) : ICommand<DeleteOrderingResult>;
    public record DeleteOrderingResult(bool IsSuccess);
    public class DeleteOrderingHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteOrderingCommand, DeleteOrderingResult>
    {
        public async Task<DeleteOrderingResult> Handle(DeleteOrderingCommand command, CancellationToken cancellationToken)
        {
            var ordering = await context.Orderings.SingleOrDefaultAsync(o => o.OrderingId == command.OrderingId, cancellationToken);
            if (ordering == null)
            {
                throw new OrderingNotFoundException(command.OrderingId);
            }

            context.Orderings.Remove(ordering);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteOrderingResult(true);

        }
    }
}
