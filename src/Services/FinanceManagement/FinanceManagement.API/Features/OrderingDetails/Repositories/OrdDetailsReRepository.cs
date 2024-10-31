namespace FinanceManagement.API.Features.OrderingDetails.Repositories
{
    public class OrdDetailsReRepository(ApplicationDbContext context)
        : IOrdDetailsReRepository
    {
        public async Task<Guid> CreateOrdDetail(OrderingDetail OrdDetail, CancellationToken cancellationToken)
        {
            context.OrderingDetails.Add(OrdDetail);
            await context.SaveChangesAsync();
            return OrdDetail.OrderingId;
        }

        public Task<bool> DeleteOrdDetail(Guid RoomId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderingDetail> GetOrdDetailById(Guid OrderingId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderingDetail>> GetOrdDetails(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
