namespace PaymentManagement.API.Features.Payments.UpdatePayment
{
    public record UpdatePaymentCommand(Guid PaymentId, Guid PaymentMethodId) 
        : ICommand<UpdatePaymentResult>;
    public record UpdatePaymentResult(bool IsSuccess);
    public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty().WithMessage("PaymentId is required");
            RuleFor(x => x.PaymentMethodId).NotEmpty().WithMessage("PaymentMethodId is required");
        }
    }
    public class UpdatePaymentHandler(ApplicationDbContext context)
        : ICommandHandler<UpdatePaymentCommand, UpdatePaymentResult>
    {
        public async Task<UpdatePaymentResult> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.SingleOrDefaultAsync(p => p.PaymentId == command.PaymentId, cancellationToken);
            if (payment is null)
            {
                throw new PaymentNotFoundException(command.PaymentId);
            }

            payment.PaymentDate = DateTime.Now;
            payment.PaymentMethodId = command.PaymentMethodId;
            payment.Status = PaymentStatus.Completed;

            context.Payments.Update(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdatePaymentResult(true);
        }
    }
}
