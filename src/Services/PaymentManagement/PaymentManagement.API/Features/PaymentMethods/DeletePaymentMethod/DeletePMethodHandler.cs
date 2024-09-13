
namespace PaymentManagement.API.Features.PaymentMethods.DeletePaymentMethod
{
    public record DeletePMethodCommand(Guid PaymentMethodId) : ICommand<DeletePMethodResult>;
    public record DeletePMethodResult(bool IsSuccess);
    public class DeletePMethodValidator : AbstractValidator<DeletePMethodCommand>
    {
        public DeletePMethodValidator()
        {
            RuleFor(x => x.PaymentMethodId).NotEmpty().WithMessage("PaymentMethodId is required");
        }
    }
    public class DeletePMethodHandler(ApplicationDbContext context)
        : ICommandHandler<DeletePMethodCommand, DeletePMethodResult>
    {
        public async Task<DeletePMethodResult> Handle(DeletePMethodCommand command, CancellationToken cancellationToken)
        {
            var method = await context.PaymentMethods.SingleOrDefaultAsync(m => m.PaymentMethodId == command.PaymentMethodId, cancellationToken);
            if(method is null)
            {
                throw new PMethodNotFoundException(command.PaymentMethodId);
            }

            context.PaymentMethods.Remove(method);
            await context.SaveChangesAsync(cancellationToken);

            return new DeletePMethodResult(true);
        }
    }
}
