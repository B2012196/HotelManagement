using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BookingManagement.API.Features.Bookings.Commands.UpdateBookingConfirm
{
    public record UpdateBookingConfirmCommand
        (Guid BookingId, Guid RoomId) : ICommand<UpdateBookingConfirmResult>;
    public record UpdateBookingConfirmResult(bool IsSuccess);

    public class UpdateBookingConfirmValidator : AbstractValidator<UpdateBookingConfirmCommand>
    {
        public UpdateBookingConfirmValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required.");
            RuleFor(x => x.RoomId).NotEmpty().WithMessage("RoomId is required.");
        }
    }
    public class UpdateBookingConfirmHandler(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateBookingConfirmCommand, UpdateBookingConfirmResult>
    {
        public async Task<UpdateBookingConfirmResult> Handle(UpdateBookingConfirmCommand command, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.FirstOrDefaultAsync(b => b.BookingId == command.BookingId, cancellationToken);

            if (booking is null)
            {
                throw new BookingNotFoundException(command.BookingId);
            }

            booking.BookingStatus = BookingStatus.Confirmed;

            var eventMessage = command.Adapt<BookingConfirmedEvent>();
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            context.Bookings.Update(booking);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateBookingConfirmResult(true);
        }

    }
}
