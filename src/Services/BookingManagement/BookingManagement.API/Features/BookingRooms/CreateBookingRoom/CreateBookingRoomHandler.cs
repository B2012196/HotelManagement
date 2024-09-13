namespace BookingManagement.API.Features.BookingRooms.CreateBookingRoom
{
    public record CreateBookingRoomCommand(Guid BookingId, Guid RoomId) : ICommand<CreateBookingRoomResult>;
    public record CreateBookingRoomResult(bool IsSuccess);
    public class CreateBookingRoomHandler(ApplicationDbContext context)
        : ICommandHandler<CreateBookingRoomCommand, CreateBookingRoomResult>
    {
        public async Task<CreateBookingRoomResult> Handle(CreateBookingRoomCommand command, CancellationToken cancellationToken)
        {
            var bookingroom = new BookingRoom
            {
                BookingId = command.BookingId,
                RoomId = command.RoomId
            };

            context.BookingRooms.Add(bookingroom);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookingRoomResult(true);

        }
    }
}
