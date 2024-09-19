namespace StaffManagement.API.Features.StaffRoles.CreateStaffRole
{
    public record CreateStaffRoleCommand(string StaffRoleName) : ICommand<CreateStaffRoleResult>;
    public record CreateStaffRoleResult(Guid StaffRoleId);
    public class CreateStaffRoleValidator : AbstractValidator<CreateStaffRoleCommand>
    {
        public CreateStaffRoleValidator()
        {
            RuleFor(x => x.StaffRoleName)
                .NotEmpty().WithMessage("HotelId is required.")
                .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");
        }
    }
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
