namespace BookingManagement.API.Features.Bookings.Commands.UpdateBookingCheckin
{
    public record UpdateBookingCheckinCommand(Guid BookingId, DateTime CheckinDate) : ICommand<UpdateBookingCheckinResult>;
    public record UpdateBookingCheckinResult(bool IsSuccess);

    public class UpdateBookingCheckinValidator : AbstractValidator<UpdateBookingCheckinCommand>
    {
        public UpdateBookingCheckinValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required.");
            RuleFor(x => x.CheckinDate).GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Check-in date cannot be in the past.");
        }
    }
    public class UpdateBookingCheckinHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateBookingCheckinCommand, UpdateBookingCheckinResult>
    {
        public async Task<UpdateBookingCheckinResult> Handle(UpdateBookingCheckinCommand command, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingId == command.BookingId, cancellationToken);
            if (booking is null)
            {
                throw new BookingNotFoundException(command.BookingId);
            }

            booking.CheckinDate = command.CheckinDate;
            booking.BookingStatus = BookingStatus.CheckedIn;

            //event BookingCheckinEvent


            context.Bookings.Update(booking);
            await context.SaveChangesAsync(cancellationToken);
            return new UpdateBookingCheckinResult(true);
        }
    }
}
