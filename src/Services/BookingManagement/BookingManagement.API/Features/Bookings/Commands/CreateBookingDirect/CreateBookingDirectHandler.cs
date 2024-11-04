namespace BookingManagement.API.Features.Bookings.Commands.CreateBookingDirect
{
    public record CreateBookingDirectCommand
        (Guid GuestId, Guid TypeId, DateTime ExpectedCheckinDate, DateTime ExpectedCheckoutDate, int RoomQuantity) : ICommand<CreateBookingDirectResult>;
    public record CreateBookingDirectResult(Guid BookingId);
    public class CreateBookingDirectHandler(ApplicationDbContext context)
        : ICommandHandler<CreateBookingDirectCommand, CreateBookingDirectResult>
    {
        public async Task<CreateBookingDirectResult> Handle(CreateBookingDirectCommand command, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                GuestId = command.GuestId,
                TypeId = command.TypeId,
                ExpectedCheckinDate = command.ExpectedCheckinDate,
                ExpectedCheckoutDate = command.ExpectedCheckoutDate,
                RoomQuantity = command.RoomQuantity,
                BookingStatus = BookingStatus.Pending
            };

            context.Bookings.Add(booking);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookingDirectResult(booking.BookingId);
        }
    }
}
