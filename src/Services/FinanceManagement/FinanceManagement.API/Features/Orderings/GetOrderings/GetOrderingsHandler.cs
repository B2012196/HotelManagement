
namespace FinanceManagement.API.Features.Orderings.GetOrderings
{
    public record GetOrderingsQuery() : IQuery<GetOrderingsResult>;
    public record GetOrderingsResult(IEnumerable<Ordering> Orderings);
    public class GetOrderingsHandler(ApplicationDbContext context)
        : IQueryHandler<GetOrderingsQuery, GetOrderingsResult>
    {
        public async Task<GetOrderingsResult> Handle(GetOrderingsQuery query, CancellationToken cancellationToken)
        {
            var orderings = await context.Orderings.ToListAsync(cancellationToken);

            return new GetOrderingsResult(orderings);
        }
    }
}
