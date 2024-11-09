namespace FinanceManagement.API.Features.VnPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
