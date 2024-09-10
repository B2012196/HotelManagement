
namespace HotelManagement.API.Features.RoomTypes.UpdateRoomType
{
    public record UpdateRoomTypeCommand 
        (Guid TypeId, string Name, string Description, decimal PricePerNight, int Capacity)
        : ICommand<UpdateRoomTypeResult> ;
    public record UpdateRoomTypeResult(bool IsSuccess);
    public class UpdateRoomTypeValidator : AbstractValidator<UpdateRoomTypeCommand>
    {
        public UpdateRoomTypeValidator()
        {
            RuleFor(x => x.TypeId).NotEmpty().WithMessage("TypeId is required.");

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
    public class UpdateRoomTypeHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateRoomTypeCommand, UpdateRoomTypeResult>
    {
        public async Task<UpdateRoomTypeResult> Handle(UpdateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            var type = await context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == command.TypeId, cancellationToken);

            if (type is null)
            {

            }

            type.Name = command.Name;
            type.Description = command.Description;
            type.PricePerNight = command.PricePerNight;
            type.Capacity = command.Capacity;

            context.Update(type);
            await context.SaveChangesAsync(cancellationToken);
            return new UpdateRoomTypeResult(true);
        }
    }
}
