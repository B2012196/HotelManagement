using StaffManagement.API.Features.Staffs.Repositories;

namespace StaffManagement.API.Features.Staffs.CreateStaff
{
    public record CreateStaffCommand
        (Guid UserId, Guid HotelId, string FirstName, string LastName, decimal Salary, DateOnly DateofBirst,
        string Address, DateOnly HireDate) : ICommand<CreateStaffResult>;
    public record CreateStaffResult(Guid StaffId);
    public class CreateStaffValidator : AbstractValidator<CreateStaffCommand>
    {
        public CreateStaffValidator()
        {
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
    public class CreateStaffHandler(IStaffRepository staffRepository)
        : ICommandHandler<CreateStaffCommand, CreateStaffResult>
    {
        public async Task<CreateStaffResult> Handle(CreateStaffCommand command, CancellationToken cancellationToken)
        {
            var staff = new Staff
            {
                StaffId = Guid.NewGuid(),
                UserId = command.UserId,
                HotelId = command.HotelId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Salary = command.Salary,
                DateofBirst = command.DateofBirst,
                Address = command.Address,
                HireDate = command.HireDate
            };

            var result = await staffRepository.CreateStaff(staff, cancellationToken);

            return new CreateStaffResult(result);
        }
    }
}
