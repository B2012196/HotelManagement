
namespace HotelManagement.API.Features.RoomStatuses.UpdateRoomStatus
{
    public record UpdateRoomStatusCommand(Guid StatusId, string Name) : ICommand<UpdateRoomStatusResult>;
    public record UpdateRoomStatusResult(bool IsSuccess);
    public class UpdateRoomStatusValidator : AbstractValidator<UpdateRoomStatusCommand>
    {
        public UpdateRoomStatusValidator()
        {
            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("StatusId is required.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("StatusName is required.");
        }
    }
    public class UpdateRoomStatusHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateRoomStatusCommand, UpdateRoomStatusResult>
    {
        public async Task<UpdateRoomStatusResult> Handle(UpdateRoomStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await context.RoomStatus.SingleOrDefaultAsync(s => s.StatusId == command.StatusId, cancellationToken);
            if(status is null)
            {
                throw new RoomStatusNotFoundException(command.StatusId);
            }

            status.Name = command.Name;

            context.RoomStatus.Update(status);
            await context.SaveChangesAsync(cancellationToken);
            return new UpdateRoomStatusResult(true);
        }
    }
}
