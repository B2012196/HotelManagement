namespace StaffManagement.API.Features.StaffRoles.CreateStaffRole
{
    public record CreateStaffRoleCommand(string StaffRoleName) : ICommand<CreateStaffRoleResult>;
    public record CreateStaffRoleResult(Guid StaffRoleId);
    public class CreateStaffRoleHandler(ApplicationDbContext context)
        : ICommandHandler<CreateStaffRoleCommand, CreateStaffRoleResult>
    {
        public async Task<CreateStaffRoleResult> Handle(CreateStaffRoleCommand command, CancellationToken cancellationToken)
        {
            var role = new StaffRole
            {
                StaffRoleId = Guid.NewGuid(),
                StaffRoleName = command.StaffRoleName,
            };

            context.StaffRoles.Add(role);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateStaffRoleResult(role.StaffRoleId);
        }
    }
}
