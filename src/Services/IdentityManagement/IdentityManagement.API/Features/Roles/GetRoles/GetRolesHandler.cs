
namespace IdentityManagement.API.Features.Roles.GetRoles
{
    public record GetRolesQuery() : IQuery<GetRolesResult>;
    public record GetRolesResult(IEnumerable<Role> Roles);
    public class GetRolesHandler(ApplicationDbContext context)
        : IQueryHandler<GetRolesQuery, GetRolesResult>
    {
        public async Task<GetRolesResult> Handle(GetRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await context.Roles.ToListAsync();  

            return new GetRolesResult(roles);
        }
    }
}
