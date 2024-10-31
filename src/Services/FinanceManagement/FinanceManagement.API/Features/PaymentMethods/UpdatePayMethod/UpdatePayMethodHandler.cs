
namespace FinanceManagement.API.Features.PaymentMethods.UpdatePayMethod
{
    public record UpdatePayMethodCommand(Guid PaymentMethodId, string PaymentMethodName) : ICommand<UpdatePayMethodResult>;
    public record UpdatePayMethodResult(bool IsSuccess);
    public class UpdatePayMethodHandler(ApplicationDbContext context)
        : ICommandHandler<UpdatePayMethodCommand, UpdatePayMethodResult>
    {
        public async Task<UpdatePayMethodResult> Handle(UpdatePayMethodCommand command, CancellationToken cancellationToken)
        {
            var paymethod = await context.PaymentMethods.SingleOrDefaultAsync(pm => pm.PaymentMethodId == command.PaymentMethodId, cancellationToken);

            if (paymethod == null)
            {
                throw new PayMethodNotFoundException(command.PaymentMethodId);
            }

            paymethod.PaymentMethodName = command.PaymentMethodName;

            context.PaymentMethods.Update(paymethod);

            await context.SaveChangesAsync(cancellationToken);

            return new UpdatePayMethodResult(true);
        }
    }
}
