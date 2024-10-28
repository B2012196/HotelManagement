
namespace FinanceManagement.API.Features.Services.GetServices
{
    public record GetServicesQuery() : IQuery<GetServicesResult>;
    public record GetServicesResult(IEnumerable<Service> Services);
    public class GetServicesHandler(ApplicationDbContext context)
        : IQueryHandler<GetServicesQuery, GetServicesResult>
    {
        public async Task<GetServicesResult> Handle(GetServicesQuery query, CancellationToken cancellationToken)
        {
            var services = await context.Services.ToListAsync(cancellationToken);

            return new GetServicesResult(services);
        }
    }
}
