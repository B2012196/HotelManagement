
namespace FinanceManagement.API.Features.PaymentMethods.DeletePayMethod
{
    public record DeletePayMethodCommand(Guid PaymentMethodId) : ICommand<DeletePayMethodResult>;
    public record DeletePayMethodResult(bool IsSuccess);
    public class DeletePayMethodHandler(ApplicationDbContext context)
        : ICommandHandler<DeletePayMethodCommand, DeletePayMethodResult>
    {
        public async Task<DeletePayMethodResult> Handle(DeletePayMethodCommand command, CancellationToken cancellationToken)
        {
            var paymethod = await context.PaymentMethods.SingleOrDefaultAsync(pm => pm.PaymentMethodId == command.PaymentMethodId, cancellationToken);

            if (paymethod == null)
            {
                throw new PayMethodNotFoundException(command.PaymentMethodId);
            }

            context.PaymentMethods.Remove(paymethod);

            await context.SaveChangesAsync(cancellationToken);

            return new DeletePayMethodResult(true);
        }
    }
}
