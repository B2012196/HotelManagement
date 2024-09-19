namespace HotelManagement.API.Features.Rooms.Commands.DeleteRoom
{
    public record DeleteRoomCommand(Guid RoomId) : ICommand<DeleteRoomResult>;
    public record DeleteRoomResult(bool IsSuccess);
    public class DeleteRoomValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("RoomId is required.");
        }
    }
    public class DeleteRoomHandler(IRoomRepository repository)
        : ICommandHandler<DeleteRoomCommand, DeleteRoomResult>
    {
        public async Task<DeleteRoomResult> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteRoom(command.RoomId, cancellationToken);

            return new DeleteRoomResult(true);
        }
    }
}
