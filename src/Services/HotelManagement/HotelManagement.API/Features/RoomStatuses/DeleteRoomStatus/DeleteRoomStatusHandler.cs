
namespace HotelManagement.API.Features.RoomStatuses.DeleteRoomStatus
{
    public record DeleteRoomStatusCommand(Guid StatusId) : ICommand<DeleteRoomStatusResult>;
    public record DeleteRoomStatusResult(bool IsSuccess);
    public class DeleteRoomStatusValidator : AbstractValidator<DeleteRoomStatusCommand>
    {
        public DeleteRoomStatusValidator()
        {
            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("StatusId is required.");
        }
    }
    public class DeleteRoomStatusHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteRoomStatusCommand, DeleteRoomStatusResult>
    {
        public async Task<DeleteRoomStatusResult> Handle(DeleteRoomStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await context.RoomStatus.SingleOrDefaultAsync(s => s.StatusId == command.StatusId, cancellationToken);
            if (status is null) { }

            context.RoomStatus.Remove(status);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteRoomStatusResult(true);
        }
    }
}
