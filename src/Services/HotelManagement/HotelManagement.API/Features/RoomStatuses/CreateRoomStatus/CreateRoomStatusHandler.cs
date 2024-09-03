namespace HotelManagement.API.Features.RoomStatuses.CreateRoomStatus
{
    public record CreateRoomStatusCommand(string Name) : ICommand<CreateRoomStatusResult>;
    public record CreateRoomStatusResult(Guid StatusId);
    public class CreateRoomStatusHandler : ICommandHandler<CreateRoomStatusCommand, CreateRoomStatusResult>
    {
        private readonly ApplicationDbContext _context;

        public CreateRoomStatusHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateRoomStatusResult> Handle(CreateRoomStatusCommand command, CancellationToken cancellationToken)
        {
            var status = new RoomStatus
            {
                StatusId = Guid.NewGuid(),
                Name = command.Name,
            };   
            
            _context.RoomStatus.Add(status);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateRoomStatusResult(status.StatusId);
        }
    }
}
