namespace FinanceManagement.API.Features.OrderingDetails.Repositories
{
    public interface IOrdDetailsReRepository
    {
        Task<IEnumerable<OrderingDetail>> GetOrdDetails(CancellationToken cancellationToken);
        Task<OrderingDetail> GetOrdDetailById(Guid OrderingId, CancellationToken cancellationToken);
        Task<Guid> CreateOrdDetail(OrderingDetail OrdDetail, CancellationToken cancellationToken);
        Task<bool> DeleteOrdDetail(Guid RoomId, CancellationToken cancellationToken);
    }
}
