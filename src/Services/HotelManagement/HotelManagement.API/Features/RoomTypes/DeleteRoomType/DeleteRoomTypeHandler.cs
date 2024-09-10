namespace HotelManagement.API.Features.RoomTypes.DeleteRoomType
{
    public record DeleteRoomTypeCommand(Guid TypeId) : ICommand<DeleteRoomTypeResult>;
    public record DeleteRoomTypeResult(bool IsSuccess);
    public class DeleteRoomTypeValidator : AbstractValidator<DeleteRoomTypeCommand>
    {
        public DeleteRoomTypeValidator()
        {
            RuleFor(x => x.TypeId)
                .NotEmpty().WithMessage("TypeId is required.");
        }
    }
    public class DeleteRoomTypeHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteRoomTypeCommand, DeleteRoomTypeResult>
    {
        public async Task<DeleteRoomTypeResult> Handle(DeleteRoomTypeCommand command, CancellationToken cancellationToken)
        {
            var type = await context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == command.TypeId, cancellationToken);
            if (type is null) { }

            context.RoomTypes.Remove(type);
            await context.SaveChangesAsync(cancellationToken);
            return new DeleteRoomTypeResult(true);

        }
    }
}
