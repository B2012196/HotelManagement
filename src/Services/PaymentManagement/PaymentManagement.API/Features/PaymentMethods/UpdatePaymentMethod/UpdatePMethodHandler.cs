namespace PaymentManagement.API.Features.PaymentMethods.UpdatePaymentMethod
{
    public record UpdatePMethodCommand(Guid PaymentMethodId, string PaymentMethodName) : ICommand<UpdatePMethodResult>;
    public record UpdatePMethodResult(bool IsSuccess);
    public class UpdatePMethodValidator : AbstractValidator<UpdatePMethodCommand>
    {
        public UpdatePMethodValidator()
        {
            RuleFor(x => x.PaymentMethodId).NotEmpty().WithMessage("PaymentMethodId is required");
            RuleFor(x => x.PaymentMethodName).NotEmpty().WithMessage("PaymentMethodName is required");
        }
    }
    public class UpdatePMethodHandler(ApplicationDbContext context)
        : ICommandHandler<UpdatePMethodCommand, UpdatePMethodResult>
    {
        public async Task<UpdatePMethodResult> Handle(UpdatePMethodCommand command, CancellationToken cancellationToken)
        {
            var method = await context.PaymentMethods.SingleOrDefaultAsync(m => m.PaymentMethodId == command.PaymentMethodId, cancellationToken);
            if (method is null)
            {
                throw new PMethodNotFoundException(command.PaymentMethodId);
            }

            method.PaymentMethodName = command.PaymentMethodName;

            context.PaymentMethods.Update(method);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdatePMethodResult(true);
        }
    }
}
