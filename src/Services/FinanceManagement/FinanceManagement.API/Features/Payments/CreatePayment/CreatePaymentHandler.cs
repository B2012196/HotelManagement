namespace FinanceManagement.API.Features.Payments.CreatePayment
{
    public record CreatePaymentCommand(Guid InvoiceId, Guid PaymentMethodId, string FullName, decimal Price) : ICommand<CreatePaymentResult>;
    public record CreatePaymentResult(string PaymentUrl, Guid PaymentId);
    public class CreatePaymentHandler(ApplicationDbContext context, IVnPayService vnPayService, IHttpContextAccessor httpContextAccessor)
        : ICommandHandler<CreatePaymentCommand, CreatePaymentResult>
    {
        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var invoice = await context.Invoices.SingleOrDefaultAsync(i => i.InvoiceId == command.InvoiceId);
            Console.WriteLine("InvoiceId: " + command.InvoiceId + " PaymentMethodId: " + command.PaymentMethodId + " Fullname: " + command.FullName);

            if(invoice == null)
            {
                throw new InvoiceNotFoundException(command.InvoiceId);
            }

            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                InvoiceId = command.InvoiceId,
                PaymentMethodId = command.PaymentMethodId,
                Amount = command.Price,
                CreateAt = DateTime.Now
            };

            var vnpay = new VnPaymentRequestModel
            {
                InvoiceId = invoice.InvoiceId,
                FullName = command.FullName,
                Description = "THANH TOAN HOA DON",
                Amount = command.Price,
                CreatedDate = DateTime.Now,
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync(cancellationToken);

            var paymentUrl = vnPayService.CreatePaymentUrl(httpContextAccessor.HttpContext, vnpay);

            return new CreatePaymentResult(paymentUrl, payment.PaymentId);

        }
    }
}
