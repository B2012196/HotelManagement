namespace PaymentManagement.API.Features.Payments.CreatePayment
{
    public record CreatePaymentCommand(Guid BookingId)
        : ICommand<CreatePaymentResult>;
    public record CreatePaymentResult(Guid PaymentId);

    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required");
        }
    }
    public class CreatePaymentHandler(ApplicationDbContext context)
        : ICommandHandler<CreatePaymentCommand, CreatePaymentResult>
    {
        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                BookingId = command.BookingId,
                Amount = 20000,
                Status = PaymentStatus.Pending,
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new CreatePaymentResult(payment.PaymentId);




        }
    }
}
