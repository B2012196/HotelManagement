namespace StaffManagement.API.Features.StaffRoles.UpdateStaffRole
{
    public record UpdateStaffRoleCommand(Guid StaffRoleId, string StaffRoleName) : ICommand<UpdateStaffRoleResult>;
    public record UpdateStaffRoleResult(bool IsSuccess);
    public class UpdateStaffRoleValidator : AbstractValidator<UpdateStaffRoleCommand>
    {
        public UpdateStaffRoleValidator()
        {
            RuleFor(x => x.StaffRoleId)
                .NotEmpty().WithMessage("StaffRoleId is required.");
            RuleFor(x => x.StaffRoleName)
                .NotEmpty().WithMessage("HotelId is required.")
                .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");
        }
    }
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
