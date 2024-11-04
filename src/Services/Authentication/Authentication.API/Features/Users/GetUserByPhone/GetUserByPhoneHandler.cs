
namespace Authentication.API.Features.Users.GetUserByPhone
{
    public record GetUserByPhoneQuery(string Phone) : IQuery<GetUserByPhoneResult>;
    public record GetUserByPhoneResult(UserDto UserDto);
    public class GetUserByPhoneHandler(ApplicationDbContext context)
        : IQueryHandler<GetUserByPhoneQuery, GetUserByPhoneResult>
    {
        public async Task<GetUserByPhoneResult> Handle(GetUserByPhoneQuery query, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.PhoneNumber == query.Phone, cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException(query.Phone);
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
            return new GetUserByPhoneResult(userDto);
        }
    }
}
