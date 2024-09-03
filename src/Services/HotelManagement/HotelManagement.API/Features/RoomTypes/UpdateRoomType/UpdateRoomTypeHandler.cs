
namespace HotelManagement.API.Features.RoomTypes.UpdateRoomType
{
    public record UpdateRoomTypeCommand 
        (Guid TypeId, string Name, string Description, decimal PricePerNight, int Capacity)
        : ICommand<UpdateRoomTypeResult> ;
    public record UpdateRoomTypeResult(bool IsSuccess);
    public class UpdateRoomTypeHandler : ICommandHandler<UpdateRoomTypeCommand, UpdateRoomTypeResult>
    {
        private readonly ApplicationDbContext _context;

        public UpdateRoomTypeHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateRoomTypeResult> Handle(UpdateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            var type = await _context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == command.TypeId, cancellationToken);

            if (type is null)
            {

            }

            type.Name = command.Name;
            type.Description = command.Description;
            type.PricePerNight = command.PricePerNight;
            type.Capacity = command.Capacity;

            _context.Update(type);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateRoomTypeResult(true);
        }
    }
}
