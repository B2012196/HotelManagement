
namespace HotelManagement.API.Features.Rooms.DeleteRoom
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
    public class DeleteRoomHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteRoomCommand, DeleteRoomResult>
    {
        public async Task<DeleteRoomResult> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
        {
            var room = await context.Rooms.SingleOrDefaultAsync(r => r.RoomId == command.RoomId, cancellationToken);
            if (room is null) { }

            context.Rooms.Remove(room);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteRoomResult(true);
        }
    }
}
