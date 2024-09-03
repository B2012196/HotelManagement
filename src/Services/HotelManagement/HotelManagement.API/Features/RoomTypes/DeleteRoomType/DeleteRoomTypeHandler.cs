
namespace HotelManagement.API.Features.RoomTypes.DeleteRoomType
{
    public record DeleteRoomTypeCommand(Guid TypeId) : ICommand<DeleteRoomTypeResult>;
    public record DeleteRoomTypeResult(bool IsSuccess);
    public class DeleteRoomTypeHandler : ICommandHandler<DeleteRoomTypeCommand, DeleteRoomTypeResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteRoomTypeHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteRoomTypeResult> Handle(DeleteRoomTypeCommand command, CancellationToken cancellationToken)
        {
            var type = await _context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == command.TypeId, cancellationToken);
            if (type is null) { }

            _context.RoomTypes.Remove(type);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteRoomTypeResult(true);

        }
    }
}
