namespace Authentication.API.Features.Users.CreateUser
{
    public record CreateUserCommand
        (Guid RoleId, string UserName, string Password, string Email, string PhoneNumber) : ICommand<CreateUserResult>;
    public record CreateUserResult(Guid UserId);
    public class CreateUserHandler(ApplicationDbContext context, IPublishEndpoint publishEndpoint, ILogger<CreateUserHandler> logger)
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

            if(role != null)
            {
                if(role.RoleName == "Guest")
                {
                    logger.LogInformation("RoleName == " + role.RoleName);
                    var createstaff = new
                    {
                        UserId = user.UserId
                    };
                    var eventMessage = createstaff.Adapt<CreateGuestEvent>();
                    await publishEndpoint.Publish(eventMessage, cancellationToken);
                }
                else
                {
                    logger.LogInformation("RoleName: " + role.RoleName);
                    var createstaff = new
                    {
                        UserId = user.UserId
                    };
                    var eventMessage = createstaff.Adapt<CreateStaffEvent>();
                    await publishEndpoint.Publish(eventMessage, cancellationToken);
                }
                
            }

            return new CreateUserResult(command.RoleId);
        }
    }
}
