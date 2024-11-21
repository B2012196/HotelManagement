namespace FinanceManagement.API.Features.Payments.CreatePaymentDirect
{
    public record CreatePayDirectCommand(Guid InvoiceId, Guid PaymentMethodId, decimal Amount) : ICommand<CreatePayDirectResult>;
    public record CreatePayDirectResult(Guid PaymentId);
    public class CreatePayDirectHandler(ApplicationDbContext context) : ICommandHandler<CreatePayDirectCommand, CreatePayDirectResult>
    {
        public async Task<CreatePayDirectResult> Handle(CreatePayDirectCommand command, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                InvoiceId = command.InvoiceId,
                PaymentMethodId = command.PaymentMethodId,
                Amount = command.Amount,
                CreateAt = DateTime.UtcNow,
            };
            
            context.Payments.Add(payment);
            await context.SaveChangesAsync(cancellationToken);
            return new CreatePayDirectResult(payment.PaymentId);
        }
    }
}
