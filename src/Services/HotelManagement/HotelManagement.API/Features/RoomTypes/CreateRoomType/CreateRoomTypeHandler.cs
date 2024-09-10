namespace HotelManagement.API.Features.RoomTypes.CreateRoomType
{
    public record CreateRoomTypeCommand(string Name, string Description, decimal PricePerNight, int Capacity) 
        : ICommand<CreateRoomTypeResult>;
    public record CreateRoomTypeResult(Guid TypeId);
    public class CreateRoomTypeValidator : AbstractValidator<CreateRoomTypeCommand>
    {
        public CreateRoomTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("RoomTypeName is required.")
                .MaximumLength(100).WithMessage("RoomTypeName cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than zero.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
        }
    }
    public class CreateRoomTypeHandler(ApplicationDbContext context)
        : ICommandHandler<CreateRoomTypeCommand, CreateRoomTypeResult>
    {
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

            context.RoomTypes.Add(type);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateRoomTypeResult(type.TypeId);
        }
    }
}
