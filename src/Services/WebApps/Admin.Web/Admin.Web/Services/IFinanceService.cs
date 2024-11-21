namespace Admin.Web.Services
{
    public interface IFinanceService
    {
        //invoice
        [Post("/finance/invoices")]
        Task<CreateInvoiceResponse> CreateInvoice(object obj);

        [Get("/finance/invoices/bookingid/{BookingId}")]
        Task<GetInvoiceByBookingIdResponse> GetInvoiceByBookingId(Guid BookingId);
        
        [Get("/finance/invoices")]
        Task<GetInvoicesResponse> GetInvoices();

        [Put("/finance/invoices")]
        Task<UpdateInvoiceResponse> UpdateInvoice(object obj);

        [Multipart]
        [Put("/finance/services/image/{ServiceId}")]
        Task<UploadRoomTypeImageResponse> UploadServiceImage(Guid ServiceId, [AliasAs("File")] StreamPart file);


        //invoicedetail
        [Get("/finance/invoicedetails")]
        Task<GetInvoiceDetailsResponse> GetInvoiceDetails();

        [Post("/finance/invoicedetails")]
        Task<CreateInvoiceDetailResponse> CreateInvoiceDetail(InvoiceDetail invoiceDetail);

        //service
        [Get("/finance/services")]
        Task<GetServicesResponse> GetServices();

        [Post("/finance/services")]
        Task<CreateServiceResponse> CreateService(Service Service);

        [Put("/finance/services")]
        Task<UpdateServiceResponse> UpdateService(Service Service);

        [Delete("/finance/services/{ServiceId}")]
        Task<DeleteServiceResponse> DeleteService(Guid ServiceId);

        //payment
        [Get("/finance/payments")]
        Task<GetPaymentsResponse> GetPayments();

        [Get("/finance/payments/invoiceid/{InvoiceId}")]
        Task<GetPayByInvoiceIdResponse> GetPayByInvoiceId(Guid InvoiceId);

        [Post("/finance/payments")]
        Task<CreatePaymentResponse> CreatePayment(object obj);

        [Post("/finance/paymentdirect")]
        Task<CreatePayDirectResponse> CreatePaymentDirect(object obj);


        [Post("/finance/vnpay")]
        Task<PaymentExecuteResponse> PaymentExecute(PaymentExecuteRequest request);
    }
}
