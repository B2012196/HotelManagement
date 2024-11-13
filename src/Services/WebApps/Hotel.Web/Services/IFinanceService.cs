namespace Hotel.Web.Services
{
    public interface IFinanceService
    {
        //invoice
        [Post("/finance/invoices")]
        Task<CreateInvoiceResponse> CreateInvoice(object obj);

        [Delete("/finance/invoices/{InvoiceId}")]
        Task<DeleteInvoiceResponse> DeleteInvoice(Guid InvoiceId);

        //payment
        [Post("/finance/payments")]
        Task<CreatePaymentResponse> CreatePayment(object obj);

        [Post("/finance/vnpay")]
        Task<PaymentExecuteResponse> PaymentExecute(PaymentExecuteRequest request);
    }
}
