namespace BookingManagement.API.Features.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand
        (Guid GuestId, DateTime ExpectedCheckinDate, DateTime ExpectedCheckoutDate)
        : ICommand<CreateBookingResult>;
    public record CreateBookingResult(Guid BookingId);

    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.GuestId).NotEmpty().WithMessage("GuestId is required.");

            // Rule for ExpectedCheckinDate: it should not be in the past
            RuleFor(x => x.ExpectedCheckinDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Expected check-in date cannot be in the past.");

            // Rule for ExpectedCheckoutDate: it should be after the ExpectedCheckinDate
            RuleFor(x => x.ExpectedCheckoutDate)
                .GreaterThan(x => x.ExpectedCheckinDate).WithMessage("Expected check-out date must be after the expected check-in date.");
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
                ExpectedCheckinDate = command.ExpectedCheckinDate,
                ExpectedCheckoutDate = command.ExpectedCheckoutDate,
                BookingStatus = BookingStatus.Pending
            };

            context.Bookings.Add(booking);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookingResult(booking.BookingId);
        }
    }
}
