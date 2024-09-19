namespace IdentityManagement.API.Features.Roles.CreateRole
{
    public record CreateRoleCommand(string RoleName) : ICommand<CreateRoleResult>;
    public record CreateRoleResult(Guid RoleId);
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("TypeId is required.")
                .MaximumLength(100).WithMessage("RoleName cannot exceed 100 characters.");
        }
    }
    public class CreateRoleHandler(ApplicationDbContext context)
        : ICommandHandler<CreateRoleCommand, CreateRoleResult>
    {
        public async Task<CreateRoleResult> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = command.RoleName,
            };

            context.Roles.Add(role);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateRoleResult(role.RoleId);

        }
    }
}
