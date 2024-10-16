
namespace Authentication.API.Features.Users.UpdatePassword
{
    public record UpdatePasswordCommand(Guid UserId, string Password, string NewPassword) : ICommand<UpdatePasswordResult>;
    public record UpdatePasswordResult(bool IsSuccess);
    public class UpdatePasswordHandler(ApplicationDbContext context)
        : ICommandHandler<UpdatePasswordCommand, UpdatePasswordResult>
    {
        public async Task<UpdatePasswordResult> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == command.UserId, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(""+command.UserId);
            };

            bool IsSuccess = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);
            if (IsSuccess)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);

                context.Users.Update(user);
                await context.SaveChangesAsync(cancellationToken);
                return new UpdatePasswordResult(true);
            }

            return new UpdatePasswordResult(false);

        }
    }
}
