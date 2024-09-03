
namespace HotelManagement.API.Features.RoomStatuses.UpdateRoomStatus
{
    public record UpdateRoomStatusCommand(Guid StatusId, string Name) : ICommand<UpdateRoomStatusResult>;
    public record UpdateRoomStatusResult(bool IsSuccess);
    public class UpdateRoomStatusHandler : ICommandHandler<UpdateRoomStatusCommand, UpdateRoomStatusResult>
    {
        private readonly ApplicationDbContext _context;

        public UpdateRoomStatusHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateRoomStatusResult> Handle(UpdateRoomStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await _context.RoomStatus.SingleOrDefaultAsync(s => s.StatusId == command.StatusId, cancellationToken);
            if(status is null) { }

            status.Name = command.Name;

            _context.RoomStatus.Update(status);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateRoomStatusResult(true);
        }
    }
}
