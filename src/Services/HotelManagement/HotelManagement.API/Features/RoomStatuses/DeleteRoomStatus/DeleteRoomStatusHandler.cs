
namespace HotelManagement.API.Features.RoomStatuses.DeleteRoomStatus
{
    public record DeleteRoomStatusCommand(Guid StatusId) : ICommand<DeleteRoomStatusResult>;
    public record DeleteRoomStatusResult(bool IsSuccess);
    public class DeleteRoomStatusHandler : ICommandHandler<DeleteRoomStatusCommand, DeleteRoomStatusResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteRoomStatusHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteRoomStatusResult> Handle(DeleteRoomStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await _context.RoomStatus.SingleOrDefaultAsync(s => s.StatusId == command.StatusId, cancellationToken);
            if (status is null) { }

            _context.RoomStatus.Remove(status);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteRoomStatusResult(true);
        }
    }
}
