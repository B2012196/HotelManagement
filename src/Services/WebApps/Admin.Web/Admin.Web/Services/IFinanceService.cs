namespace Admin.Web.Services
{
    public interface IFinanceService
    {
        //invoice
        [Post("/finance/invoices")]
        Task<CreateInvoiceResponse> CreateOrdering(object obj);

        //invoicedetail
        [Post("/finance/invoicedetails")]
        Task<CreateInvoiceResponse> Createinvoicedetail(object obj);

        //service
        [Get("/finance/services")]
        Task<GetServicesResponse> GetServices();

        [Post("/finance/services")]
        Task<CreateServiceResponse> CreateService(Service Service);

        [Put("/finance/services")]
        Task<UpdateServiceResponse> UpdateService(Service Service);

        [Delete("/finance/services/{ServiceId}")]
        Task<DeleteServiceResponse> DeleteService(Guid ServiceId);
    }
}
