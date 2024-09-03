namespace HotelManagement.API.Features.RoomTypes.CreateRoomType
{
    public record CreateRoomTypeCommand(string Name, string Description, decimal PricePerNight, int Capacity) 
        : ICommand<CreateRoomTypeResult>;
    public record CreateRoomTypeResult(Guid TypeId);
    public class CreateRoomTypeHandler : ICommandHandler<CreateRoomTypeCommand, CreateRoomTypeResult>
    {
        private readonly ApplicationDbContext _context;

        public CreateRoomTypeHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateRoomTypeResult> Handle(CreateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            var type = new RoomType
            {
                TypeId = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                PricePerNight = command.PricePerNight,
                Capacity = command.Capacity
            };

            _context.RoomTypes.Add(type);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateRoomTypeResult(type.TypeId);
        }
    }
}
