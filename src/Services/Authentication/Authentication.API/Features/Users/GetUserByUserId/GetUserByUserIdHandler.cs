namespace Authentication.API.Features.Users.GetUserByUserId
{
    public record GetUserByUserIdQuery(Guid UserId) : IQuery<GetUserByUserIdResult>;
    public record GetUserByUserIdResult(UserDto User);
    public class GetUserByUserIdHandler(ApplicationDbContext context)
        : IQueryHandler<GetUserByUserIdQuery, GetUserByUserIdResult>
    {
        public async Task<GetUserByUserIdResult> Handle(GetUserByUserIdQuery query, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == query.UserId, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(""+query.UserId);
            }
            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FailedLoginAttempt = user.FailedLoginAttempt,
                IsActive = user.IsActive,
                CreateAt = user.CreateAt,
                RoleId = user.RoleId,
            };

            return new GetUserByUserIdResult(userDto);
        }
    }
}
