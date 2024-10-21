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
    public class DeleteStaffHandler(IStaffRepository staffRepository)
        : ICommandHandler<DeleteStaffCommand, DeleteStaffResult>
    {
        public async Task<DeleteStaffResult> Handle(DeleteStaffCommand command, CancellationToken cancellationToken)
        {
            var result = await staffRepository.DeleteStaff(command.StaffId, cancellationToken);

            return new DeleteStaffResult(result);
        }
    }
}
