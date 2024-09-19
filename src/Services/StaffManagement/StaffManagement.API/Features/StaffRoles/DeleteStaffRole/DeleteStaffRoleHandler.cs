namespace StaffManagement.API.Features.StaffRoles.DeleteStaffRole
{
    public record DeleteStaffRoleCommand(Guid StaffRoleId) : ICommand<DeleteStaffRoleResult>;
    public record DeleteStaffRoleResult(bool IsSuccess);
    public class DeleteStaffRoleValidator : AbstractValidator<DeleteStaffRoleCommand>
    {
        public DeleteStaffRoleValidator()
        {
            RuleFor(x => x.StaffRoleId)
                .NotEmpty().WithMessage("StaffRoleId is required.");
        }
    }
    public class DeleteStaffRoleHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteStaffRoleCommand, DeleteStaffRoleResult>
    {
        public async Task<DeleteStaffRoleResult> Handle(DeleteStaffRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var role = await context.StaffRoles.SingleOrDefaultAsync(sr => sr.StaffRoleId == command.StaffRoleId, cancellationToken);

                if (role is null)
                {
                    throw new StaffRoleNotFoundException(command.StaffRoleId);
                }

                context.StaffRoles.Remove(role);
                await context.SaveChangesAsync(cancellationToken);

                return new DeleteStaffRoleResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
    }
}
