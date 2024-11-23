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
                throw new BookingNotFoundException(command.BookingId+"");
            }
            //change date and status
            booking.CheckinDate = DateTime.UtcNow;
            booking.BookingStatus = BookingStatus.CheckedIn;


            var bookingrooms = await context.BookingRooms.Where(r => r.BookingId == command.BookingId).ToListAsync(cancellationToken);
            if (bookingrooms.Any())
            {
                List<Guid> RoomIds = new List<Guid>();
                foreach (var bookroom in bookingrooms)
                {
                    RoomIds.Add(bookroom.RoomId);
                }
                var eventObj = new
                {
                    BookingId = command.BookingId,
                    RoomIds = RoomIds
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
                throw new BookingNotFoundException(command.BookingId+"");
            }
            
        }
    }
}
