using BuildingBlocks.Messaging.Events;

namespace BookingManagement.API.Features.Bookings.Commands.UpdateBookingCheckin
{
    public record UpdateBookingCheckinCommand(Guid BookingId) : ICommand<UpdateBookingCheckinResult>;
    public record UpdateBookingCheckinResult(bool IsSuccess);

    public class UpdateBookingCheckinValidator : AbstractValidator<UpdateBookingCheckinCommand>
    {
        public UpdateBookingCheckinValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required.");
        }
    }
    public class UpdateBookingCheckinHandler(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateBookingCheckinCommand, UpdateBookingCheckinResult>
    {
        public async Task<UpdateBookingCheckinResult> Handle(UpdateBookingCheckinCommand command, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingId == command.BookingId, cancellationToken);
            if (booking is null)
            {
                throw new BookingNotFoundException(command.BookingId);
            }

            booking.CheckinDate = DateTime.Now;
            booking.BookingStatus = BookingStatus.CheckedIn;

            var bookingrooms = await context.BookingRooms.Where(r => r.BookingId == command.BookingId).ToListAsync(cancellationToken);
            if (bookingrooms.Any())
            {
                var roomId = bookingrooms[0].RoomId;
                var eventObj = new
                {
                    BookingId = command.BookingId,
                    RoomId = roomId
                };
                //event BookingCheckinEvent
                var eventMessage = eventObj.Adapt<BookingCheckinEvent>();
                await publishEndpoint.Publish(eventMessage, cancellationToken);

                //update database
                context.Bookings.Update(booking);
                await context.SaveChangesAsync(cancellationToken);
                return new UpdateBookingCheckinResult(true);
            }
            else
            {
                throw new BookingNotFoundException(command.BookingId);
            }
            
        }
    }
}
