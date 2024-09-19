namespace StaffManagement.API.Features.Staffs.UpdateStaff
{
    public record UpdateStaffCommand(Guid StaffId, Guid HotelId, Guid StaffRoleId, string FirstName, string LastName, DateOnly DateofBirst,
        string Phone, string Address, string Email) : ICommand<UpdateStaffResult>;
    public record UpdateStaffResult(bool IsSuccess);
    public class UpdateStaffValidator : AbstractValidator<UpdateStaffCommand>
    {
        public UpdateStaffValidator()
        {
            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage("StaffId is required.");

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("HotelId is required.");

            RuleFor(x => x.StaffRoleId)
                .NotEmpty().WithMessage("StaffRoleId is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(20).WithMessage("Last Name must not exceed 20 characters.");

            RuleFor(x => x.DateofBirst)
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid."); // E.164 format

            // Address không được để trống
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            // Email không được để trống và phải là định dạng email hợp lệ
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.");
        }
        // Hàm kiểm tra DateOnly có hợp lệ không
        private bool BeAValidDate(DateOnly date)
        {
            return date != default;
        }
    }
    public class UpdateStaffHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateStaffCommand, UpdateStaffResult>
    {
        public async Task<UpdateStaffResult> Handle(UpdateStaffCommand command, CancellationToken cancellationToken)
        {
            var staff = await context.Staffs.SingleOrDefaultAsync(s => s.StaffId == command.StaffId, cancellationToken);

            if (staff is null)
            {
                throw new StaffNotFoundException(command.StaffId);
            }

            staff.HotelId = command.HotelId;
            staff.StaffRoleId = command.StaffRoleId;
            staff.FirstName = command.FirstName;
            staff.LastName = command.LastName;
            staff.DateofBirst = command.DateofBirst;
            staff.Phone = command.Phone;
            staff.Address = command.Address;
            staff.Email = command.Email;

            context.Staffs.Update(staff);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateStaffResult(true);

        }
    }
}
