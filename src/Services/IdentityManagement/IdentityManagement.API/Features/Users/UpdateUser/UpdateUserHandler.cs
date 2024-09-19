namespace IdentityManagement.API.Features.Users.UpdateUser
{
    public record UpdateUserCommand(Guid RoleId, string UserName, string Password, string Email, string PhoneNumber, bool IsActive) 
        : ICommand<UpdateUserResult>;
    public record UpdateUserResult(bool IsSuccess);
    public class UpdateUserHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == command.UserName, cancellationToken);

            if(user is null)
            {
                throw new UserNotFoundException(command.UserName);
            };

            user.RoleId = command.RoleId;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            user.Email = command.Email;
            user.PhoneNumber = command.PhoneNumber;
            user.IsActive = command.IsActive;

            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateUserResult(true);

        }
    }
}
