namespace IdentityManagement.API.Features.Users.GetUsers
{
    public record GetUsersQuery() : IQuery<GetUsersResult>;
    public record GetUsersResult(IEnumerable<UserDto> UserDtos);
    public class GetUsersHandler(ApplicationDbContext context)
        : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await context.Users.Select(x => new UserDto
            {
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                FailedLoginAttempt = x.FailedLoginAttempt,
                IsActive = x.IsActive,
                CreateAt = x.CreateAt,
                RoleId = x.RoleId,
            }).ToListAsync(cancellationToken);

            return new GetUsersResult(users);
        }
    }
}
