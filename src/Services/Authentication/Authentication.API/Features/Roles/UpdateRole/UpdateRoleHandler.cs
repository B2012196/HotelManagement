namespace Authentication.API.Features.Roles.UpdateRole
{
    public record UpdateRoleCommand(Guid RoleId, string RoleName) : ICommand<UpdateRoleResult>;
    public record UpdateRoleResult(bool IsSuccess);
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.");

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("TypeId is required.")
                .MaximumLength(100).WithMessage("RoleName cannot exceed 100 characters.");
        }
    }
    public class UpdateRoleHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateRoleCommand, UpdateRoleResult>
    {
        public async Task<UpdateRoleResult> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == command.RoleId, cancellationToken);

            if (role is null)
            {
                throw new RoleNotFoundException(command.RoleId);
            }

            role.RoleName = command.RoleName;

            context.Roles.Update(role);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateRoleResult(true);
        }
    }
}
