
namespace FinanceManagement.API.Features.Payments.UpdatePayment
{
    public record UpdatePaymentCommand(Guid PaymentId, decimal Amount) : ICommand<UpdatePaymentResult>;
    public record UpdatePaymentResult(bool IsSuccess);
    public class UpdatePaymentHandler(ApplicationDbContext context)
        : ICommandHandler<UpdatePaymentCommand, UpdatePaymentResult>
    {
        public async Task<UpdatePaymentResult> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.SingleOrDefaultAsync(p => p.PaymentId == command.PaymentId, cancellationToken);

            if (payment == null)
            {
                throw new PaymentNotFoundException(command.PaymentId);
            }

            payment.Amount = command.Amount;

            context.Payments.Update(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdatePaymentResult(true);
        }
    }
}
