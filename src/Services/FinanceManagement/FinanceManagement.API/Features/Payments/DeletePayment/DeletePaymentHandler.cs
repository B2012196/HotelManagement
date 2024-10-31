
namespace FinanceManagement.API.Features.Payments.DeletePayment
{
    public record DeletePaymentCommand(Guid PaymentId) : ICommand<DeletePaymentResult>;
    public record DeletePaymentResult(bool IsSuccess);
    public class DeletePaymentHandler(ApplicationDbContext context)
        : ICommandHandler<DeletePaymentCommand, DeletePaymentResult>
    {
        public async Task<DeletePaymentResult> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.SingleOrDefaultAsync(p => p.PaymentId == command.PaymentId, cancellationToken);

            if (payment == null)
            {
                throw new PaymentNotFoundException(command.PaymentId);
            }

            context.Payments.Remove(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new DeletePaymentResult(true);
        }
    }
}
