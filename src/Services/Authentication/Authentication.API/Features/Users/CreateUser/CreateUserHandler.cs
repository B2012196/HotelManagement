namespace Authentication.API.Features.Users.CreateUser
{
    public record CreateUserCommand
        (Guid RoleId, string UserName, string Password, string Email, string PhoneNumber) : ICommand<CreateUserResult>;
    public record CreateUserResult(Guid UserId);
    public class CreateUserHandler(ApplicationDbContext context)
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                RoleId = command.RoleId,
                UserName = command.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                IsActive = true,
                CreateAt = DateTime.Now
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
            
            var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == user.RoleId, cancellationToken);

            if(role != null && role.RoleName == "Admin")
            {

            }

            return new CreateUserResult(command.RoleId);
        }
    }
}
