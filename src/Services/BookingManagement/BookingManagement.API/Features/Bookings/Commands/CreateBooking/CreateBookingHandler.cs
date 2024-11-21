namespace BookingManagement.API.Features.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand(Guid GuestId, DateTime ExpectedCheckinDate, DateTime ExpectedCheckoutDate, Guid TypeId, int RoomQuantity) 
        : ICommand<CreateBookingResult>;
    public record CreateBookingResult(Guid BookingId);

    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.GuestId).NotEmpty().WithMessage("GuestId is required.");
        }
    }

    public class CreateBookingHandler(ApplicationDbContext context)
        : ICommandHandler<CreateBookingCommand, CreateBookingResult>
    {
        public async Task<CreateBookingResult> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {

            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                GuestId = command.GuestId,
                TypeId = command.TypeId,
                ExpectedCheckinDate = command.ExpectedCheckinDate.ToUniversalTime(),
                ExpectedCheckoutDate = command.ExpectedCheckoutDate.ToUniversalTime(),
                RoomQuantity = command.RoomQuantity,
                BookingStatus = BookingStatus.Pending
            };
            booking.BookingCode = $"BOOK-{DateTime.Now:yyyyMMdd}-{booking.BookingId.ToString().Substring(0, 3)}";
            context.Bookings.Add(booking);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookingResult(booking.BookingId);
        }
    }
}
