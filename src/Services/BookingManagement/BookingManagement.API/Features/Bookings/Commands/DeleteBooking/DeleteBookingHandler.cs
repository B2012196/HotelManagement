namespace BookingManagement.API.Features.Bookings.Commands.DeleteBooking
{
    public record DeleteBookingCommand(Guid BookingId) : ICommand<DeleteBookingResult>;
    public record DeleteBookingResult(bool IsSuccess);
    public class DeleteBookingHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteBookingCommand, DeleteBookingResult>
    {
        public async Task<DeleteBookingResult> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingId == command.BookingId, cancellationToken);

            if (booking is null)
            {
                throw new BookingNotFoundException(command.BookingId);
            }

            context.Bookings.Remove(booking);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteBookingResult(true);
        }
    }
}
