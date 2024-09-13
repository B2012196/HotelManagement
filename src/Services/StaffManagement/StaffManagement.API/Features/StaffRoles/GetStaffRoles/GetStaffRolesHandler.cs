namespace StaffManagement.API.Features.StaffRoles.GetStaffRoles
{
    public record GetStaffRolesQuery() : IQuery<GetStaffRolesResult>;
    public record GetStaffRolesResult(IEnumerable<StaffRole> StaffRoles);
    public class GetStaffRolesHandler(ApplicationDbContext context)
        : IQueryHandler<GetStaffRolesQuery, GetStaffRolesResult>
    {
        public async Task<GetStaffRolesResult> Handle(GetStaffRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await context.StaffRoles.ToListAsync(cancellationToken);

            return new GetStaffRolesResult(roles);
        }
    }
}
