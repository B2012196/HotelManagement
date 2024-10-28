
namespace StaffManagement.API.Features.Staffs.EventHandlers.Integration
{
    public class CreateStaffConsumer(IStaffRepository repository) : IConsumer<CreateStaffEvent>
    {
        public async Task Consume(ConsumeContext<CreateStaffEvent> context)
        {
            var eventMessage = context.Message;

            var staff = new Staff
            {
                StaffId = Guid.NewGuid(),
                UserId = eventMessage.UserId,
                HotelId = Guid.Parse("15c7bc29-7380-43f8-87bb-bf062bcf5b14"),
                FirstName = string.Empty,   
                LastName = string.Empty,
                Salary = 0,
                DateofBirst = DateOnly.MinValue,
                Address = string.Empty,
                HireDate = DateOnly.FromDateTime(DateTime.Now)
            };

            await repository.CreateStaff(staff, context.CancellationToken);
        }
    }
}
