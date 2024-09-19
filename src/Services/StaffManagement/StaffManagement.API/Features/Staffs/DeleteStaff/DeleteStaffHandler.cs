
namespace StaffManagement.API.Features.Staffs.DeleteStaff
{
    public record DeleteStaffCommand(Guid StaffId) : ICommand<DeleteStaffResult>;
    public record DeleteStaffResult(bool IsSuccess);
    public class DeleteStaffValidator : AbstractValidator<DeleteStaffCommand>
    {
        public DeleteStaffValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage("StaffId is required.");
        }
    }
    public class DeleteStaffHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteStaffCommand, DeleteStaffResult>
    {
        public async Task<DeleteStaffResult> Handle(DeleteStaffCommand command, CancellationToken cancellationToken)
        {
            var staff = await context.Staffs.SingleOrDefaultAsync(s => s.StaffId == command.StaffId, cancellationToken);

            if(staff is null)
            {
                throw new StaffNotFoundException(command.StaffId);
            }

            context.Staffs.Remove(staff);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteStaffResult(true);
        }
    }
}
