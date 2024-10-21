
namespace StaffManagement.API.Features.Staffs.GetStaffs
{
    public record GetStaffsQuery() : IQuery<GetStaffsResult>;
    public record GetStaffsResult(IEnumerable<Staff> Staffs);
    public class GetStaffsHandler(IStaffRepository staffRepository)
        : IQueryHandler<GetStaffsQuery, GetStaffsResult>
    {
        public async Task<GetStaffsResult> Handle(GetStaffsQuery query, CancellationToken cancellationToken)
        {
            var result = await staffRepository.GetStaffs(cancellationToken);

            return new GetStaffsResult(result);
        }
    }
}
