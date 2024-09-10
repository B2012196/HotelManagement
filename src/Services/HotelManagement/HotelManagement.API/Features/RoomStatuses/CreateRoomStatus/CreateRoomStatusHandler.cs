namespace HotelManagement.API.Features.RoomStatuses.CreateRoomStatus
{
    public record CreateRoomStatusCommand(string Name) : ICommand<CreateRoomStatusResult>;
    public record CreateRoomStatusResult(Guid StatusId);
    public class CreateRoomStatusValidator : AbstractValidator<CreateRoomStatusCommand>
    {
        public CreateRoomStatusValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("StatusName is required.");
        }
    }
    public class CreateRoomStatusHandler(ApplicationDbContext context)
        : ICommandHandler<CreateRoomStatusCommand, CreateRoomStatusResult>
    {
        public async Task<CreateRoomStatusResult> Handle(CreateRoomStatusCommand command, CancellationToken cancellationToken)
        {
            var status = new RoomStatus
            {
                StatusId = Guid.NewGuid(),
                Name = command.Name,
            };   
            
            context.RoomStatus.Add(status);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateRoomStatusResult(status.StatusId);
        }
    }
}
