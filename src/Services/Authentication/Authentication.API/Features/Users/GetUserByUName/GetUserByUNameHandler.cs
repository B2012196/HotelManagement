
namespace Authentication.API.Features.Users.GetUserByUName
{
    public record GetUserByUNameQuery(string UserName) : IQuery<GetUserByUNameResult>;
    public record GetUserByUNameResult(User User);
    public class GetUserByUNameHandler(ApplicationDbContext context)
        : IQueryHandler<GetUserByUNameQuery, GetUserByUNameResult>
    {
        public async Task<GetUserByUNameResult> Handle(GetUserByUNameQuery query, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == query.UserName, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(query.UserName);
            }

            return new GetUserByUNameResult(user);
        }
    }
}
