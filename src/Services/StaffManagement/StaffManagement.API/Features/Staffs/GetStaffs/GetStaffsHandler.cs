
namespace StaffManagement.API.Features.Staffs.GetStaffs
{
    public record GetStaffsQuery() : IQuery<GetStaffsResult>;
    public record GetStaffsResult(IEnumerable<Staff> Staffs);
    public class GetStaffsHandler(ApplicationDbContext context)
        : IQueryHandler<GetStaffsQuery, GetStaffsResult>
    {
        public async Task<GetStaffsResult> Handle(GetStaffsQuery query, CancellationToken cancellationToken)
        {
            var queries = await context.Staffs.ToListAsync(cancellationToken);

            return new GetStaffsResult(queries);
        }
    }
}
