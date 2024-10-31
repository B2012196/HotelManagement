namespace Admin.Web.Services
{
    public interface IFinanceService
    {
        //ordering
        [Post("/finance/orderings")]
        Task<CreateOrderingResponse> CreateOrdering(object obj);

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
