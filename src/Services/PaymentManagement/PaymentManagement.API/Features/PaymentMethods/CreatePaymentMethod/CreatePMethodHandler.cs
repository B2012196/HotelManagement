namespace PaymentManagement.API.Features.PaymentMethods.CreatePaymentMethod
{
    public record CreatePMethodCommand(string Name) : ICommand<CreatePMethodResult>;
    public record CreatePMethodResult(Guid PaymentMethodId);
    public class CreatePMethodValidator : AbstractValidator<CreatePMethodCommand>
    {
        public CreatePMethodValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Paymentmethod name is required");
        }
    }
    public class CreatePMethodHandler(ApplicationDbContext context)
        : ICommandHandler<CreatePMethodCommand, CreatePMethodResult>
    {
        public async Task<CreatePMethodResult> Handle(CreatePMethodCommand command, CancellationToken cancellationToken)
        {
            var method = new PaymentMethod
            {
                PaymentMethodId = Guid.NewGuid(),
                PaymentMethodName = command.Name
            };

            context.PaymentMethods.Add(method);
            await context.SaveChangesAsync(cancellationToken);

            return new CreatePMethodResult(method.PaymentMethodId);
        }
    }
}
