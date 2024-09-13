namespace StaffManagement.API.Features.Staffs.CreateStaff
{
    public record CreateStaffCommand
        (Guid HotelId, Guid StaffRoleId, string FirstName, string LastName, DateOnly DateofBirst, 
        string Phone, string Address, string Email) : ICommand<CreateStaffResult>;
    public record CreateStaffResult(Guid StaffId);
    public class CreateStaffHandler(ApplicationDbContext context)
        : ICommandHandler<CreateStaffCommand, CreateStaffResult>
    {
        public async Task<CreateStaffResult> Handle(CreateStaffCommand command, CancellationToken cancellationToken)
        {
            var staff = new Staff
            {
                StaffId = Guid.NewGuid(),   
                HotelId = command.HotelId,
                StaffRoleId = command.StaffRoleId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                DateofBirst = command.DateofBirst,
                Phone = command.Phone,
                Address = command.Address,
                Email = command.Email
            };

            context.Staffs.Add(staff);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateStaffResult(staff.StaffId);
        }
    }
}
