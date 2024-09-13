namespace StaffManagement.API.Features.StaffRoles.UpdateStaffRole
{
    public record UpdateStaffRoleCommand(Guid StaffRoleId, string StaffRoleName) : ICommand<UpdateStaffRoleResult>;
    public record UpdateStaffRoleResult(bool IsSuccess);
    public class UpdateStaffRoleHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateStaffRoleCommand, UpdateStaffRoleResult>
    {
        public async Task<UpdateStaffRoleResult> Handle(UpdateStaffRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await context.StaffRoles.SingleOrDefaultAsync(sr => sr.StaffRoleId == command.StaffRoleId, cancellationToken);
            if (role is null)
            {
                throw new StaffRoleNotFoundException(command.StaffRoleId);
            }

            role.StaffRoleName = command.StaffRoleName;

            context.StaffRoles.Update(role);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateStaffRoleResult(true);
        }
    }
}
