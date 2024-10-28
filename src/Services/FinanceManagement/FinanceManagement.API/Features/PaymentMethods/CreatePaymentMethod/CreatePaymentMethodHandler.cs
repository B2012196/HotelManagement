
namespace FinanceManagement.API.Features.PaymentMethods.CreatePaymentMethod
{
    public record CreatePaymentMethodCommand(string PaymentMethodName) : ICommand<CreatePaymentMethodResult>;
    public record CreatePaymentMethodResult(Guid PaymentMethodId);
    public class CreatePaymentMethodHandler(ApplicationDbContext context)
        : ICommandHandler<CreatePaymentMethodCommand, CreatePaymentMethodResult>
    {
        public async Task<CreatePaymentMethodResult> Handle(CreatePaymentMethodCommand command, CancellationToken cancellationToken)
        {
            var paymethod = new PaymentMethod
            {
                PaymentMethodId = Guid.NewGuid(),
                PaymentMethodName = command.PaymentMethodName,
            };

            context.PaymentMethods.Add(paymethod);
            await context.SaveChangesAsync(cancellationToken);

            return new CreatePaymentMethodResult(paymethod.PaymentMethodId);
        }
    }
}
