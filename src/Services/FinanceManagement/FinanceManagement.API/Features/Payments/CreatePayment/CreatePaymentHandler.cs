namespace FinanceManagement.API.Features.Payments.CreatePayment
{
    public record CreatePaymentCommand(Guid InvoiceId, Guid PaymentMethodId) : ICommand<CreatePaymentResult>;
    public record CreatePaymentResult(Guid PaymentId);
    public class CreatePaymentHandler(ApplicationDbContext context)
        : ICommandHandler<CreatePaymentCommand, CreatePaymentResult>
    {
        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                InvoiceId = command.InvoiceId,
                PaymentMethodId = command.PaymentMethodId,
                Amount = 0,
                CreateAt = DateTime.Now
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new CreatePaymentResult(payment.PaymentId);

        }
    }
}
