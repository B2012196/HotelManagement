using FinanceManagement.API.Features.VnPay;

namespace FinanceManagement.API.Features.Payments.CreatePayment
{
    public record CreatePaymentCommand(Guid InvoiceId, Guid PaymentMethodId) : ICommand<CreatePaymentResult>;
    public record CreatePaymentResult(Guid PaymentId, string PaymentUrl);
    public class CreatePaymentHandler(ApplicationDbContext context, IVnPayService vnPayService, IHttpContextAccessor httpContextAccessor)
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

            var vnpay = new VnPaymentRequestModel
            {
                InvoiceId = 1,
                FullName = "Lam Minh Duc",
                Description = "Thanh toan hoa don",
                Amount = 150000,
                CreatedDate = DateTime.Now,
            };

            var paymentUrl = vnPayService.CreatePaymentUrl(httpContextAccessor.HttpContext, vnpay);

            context.Payments.Add(payment);
            await context.SaveChangesAsync(cancellationToken);

            return new CreatePaymentResult(payment.PaymentId, paymentUrl);

        }
    }
}
