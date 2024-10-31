
namespace FinanceManagement.API.Features.Services.GetServiceById
{
    public record GetServiceByIdQuery(Guid ServiceId) : IQuery<GetServiceByIdResult>;
    public record GetServiceByIdResult(Service Service);
    public class GetServiceByIdHandler(ApplicationDbContext context)
        : IQueryHandler<GetServiceByIdQuery, GetServiceByIdResult>
    {
        public async Task<GetServiceByIdResult> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == query.ServiceId, cancellationToken);

            if(service == null)
            {
                throw new ServiceNotFoundException(query.ServiceId);
            }

            return new GetServiceByIdResult(service);
        }
    }
}
