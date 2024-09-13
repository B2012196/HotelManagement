namespace PaymentManagement.API.Features.Payments.DeletePayment
{
    public record DeletePaymentCommand(Guid PaymentId) : ICommand<DeletePaymentResult>;
    public record DeletePaymentResult(bool IsSuccess);
    public class DeletePaymentValidator : AbstractValidator<DeletePaymentCommand>
    {
        public DeletePaymentValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty().WithMessage("PaymentId is required");
        }
    }
    public class DeletePaymentHandler(ApplicationDbContext context)
        : ICommandHandler<DeletePaymentCommand, DeletePaymentResult>
    {
        public async Task<DeletePaymentResult> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.SingleOrDefaultAsync(cancellationToken);
            if (payment is null)
            {
                throw new PaymentNotFoundException(command.PaymentId);
            }

            context.Payments.Remove(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new DeletePaymentResult(true);
        }
    }
}
