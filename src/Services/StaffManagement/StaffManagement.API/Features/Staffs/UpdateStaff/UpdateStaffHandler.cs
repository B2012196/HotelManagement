namespace StaffManagement.API.Features.Staffs.UpdateStaff
{
    public record UpdateStaffCommand(Guid StaffId, Guid UserId, Guid HotelId, string FirstName, string LastName, decimal Salary, DateOnly DateofBirst,
        string Address, DateOnly HireDate) : ICommand<UpdateStaffResult>;
    public record UpdateStaffResult(bool IsSuccess);
    public class UpdateStaffValidator : AbstractValidator<UpdateStaffCommand>
    {
        public UpdateStaffValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage("StaffId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("StaffRoleId is required.");

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("HotelId is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(20).WithMessage("Last Name must not exceed 20 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Price per night must be greater than zero.");

            RuleFor(x => x.DateofBirst)
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth cannot be in the future.");

            // Address không được để trống
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            // Email không được để trống và phải là định dạng email hợp lệ
            RuleFor(x => x.HireDate)
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.");
        }
        // Hàm kiểm tra DateOnly có hợp lệ không
        private bool BeAValidDate(DateOnly date)
        {
            return date != default;
        }
    }
    public class UpdateStaffHandler(IStaffRepository repository)
        : ICommandHandler<UpdateStaffCommand, UpdateStaffResult>
    {
        public async Task<UpdateStaffResult> Handle(UpdateStaffCommand command, CancellationToken cancellationToken)
        {
            var staff = await repository.GetStaffById(command.StaffId, cancellationToken);

            if (staff is null)
            {
                throw new StaffNotFoundException(command.StaffId);
            }

            staff.UserId = command.UserId;
            staff.HotelId = command.HotelId;
            staff.FirstName = command.FirstName;
            staff.LastName = command.LastName;
            staff.Salary = command.Salary;
            staff.DateofBirst = command.DateofBirst;
            staff.Address = command.Address;
            staff.HireDate = command.HireDate;

            var result = await repository.UpdateStaff(staff, cancellationToken);
            return new UpdateStaffResult(result);

        }
    }
}
