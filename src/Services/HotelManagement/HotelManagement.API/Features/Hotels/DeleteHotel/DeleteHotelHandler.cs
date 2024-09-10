namespace HotelManagement.API.Features.Hotels.DeleteHotel
{
    public record DeleteHotelCommand(Guid HotelId) : ICommand<DeleteHotelResult>;
    public record DeleteHotelResult(bool IsSuccess);

    public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelCommand>
    {
        public DeleteHotelCommandValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty().WithMessage("Hotel ID is required");
        }
    }

    public class DeleteHotelHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteHotelCommand, DeleteHotelResult>
    {
        public async Task<DeleteHotelResult> Handle(DeleteHotelCommand command, CancellationToken cancellationToken)
        {
            var hotel = await context.Hotels.SingleOrDefaultAsync(h => h.HotelId == command.HotelId, cancellationToken);

            if (hotel is null)
            {
                throw new HotelNotFoundException(command.HotelId);
            }

            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteHotelResult(true);
        }
    }
}
