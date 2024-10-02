namespace Authentication.API.Features.Roles.DeleteRole
{
    public record DeleteRoleCommand(Guid RoleId) : ICommand<DeleteRoleResult>;
    public record DeleteRoleResult(bool IsSuccess);
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.");
        }
    }
    public class DeleteRoleHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteRoleCommand, DeleteRoleResult>
    {
        public async Task<DeleteRoleResult> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == command.RoleId, cancellationToken);

            if (role is null)
            {
                throw new RoleNotFoundException(command.RoleId);
            }

            context.Roles.Remove(role);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteRoleResult(true);
        }
    }
}
