namespace FinanceManagement.API.Features.OrderingDetails.GetOrderingDetails
{
    public record GetOrdDetailsQuery() : IQuery<GetOrdDetailsResult>;
    public record GetOrdDetailsResult(IEnumerable<OrderingDetail> OrderingDetails);
    public class GetOrdDetailsHandler(ApplicationDbContext context)
        : IQueryHandler<GetOrdDetailsQuery, GetOrdDetailsResult>
    {
        public async Task<GetOrdDetailsResult> Handle(GetOrdDetailsQuery query, CancellationToken cancellationToken)
        {
            var details = await context.OrderingDetails.ToListAsync(cancellationToken);

            return new GetOrdDetailsResult(details);
        }
    }
}
