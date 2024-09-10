namespace HotelManagement.API.Features.Rooms.CreateRoom
{
    public record CreateRoomCommand
        (string Number, Guid HotelId, Guid TypeId, Guid StatusId) : ICommand<CreateRoomResult>;
    public record CreateRoomResult(Guid RoomId);

    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Number).NotEmpty().WithMessage("Room number is required.")
                .MaximumLength(10).WithMessage("Room number cannot exceed 10 characters.");

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("HotelId is required.");

            RuleFor(x => x.TypeId)
                .NotEmpty().WithMessage("TypeId is required.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("StatusId is required.");
        }
    }
    public class CreateRoomHandler(ApplicationDbContext context)
        : ICommandHandler<CreateRoomCommand, CreateRoomResult>
    {
        public async Task<CreateRoomResult> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
        {
            //create room
            var room = new Room
            {
                RoomId = Guid.NewGuid(),
                Number = command.Number,
                HotelId = command.HotelId,
                TypeId = command.TypeId,
                StatusId = command.StatusId
            };

            //save database
            context.Rooms.Add(room);
            await context.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateRoomResult(room.RoomId);
        }
    }
}
