namespace BookingManagement.API.Features.BookingRooms.CreateBookingRoom
{
    public record CreateBookingRoomCommand(Guid BookingId, List<Guid> RoomIds) : ICommand<CreateBookingRoomResult>;
    public record CreateBookingRoomResult(bool IsSuccess, List<Guid> RoomIds);
    public class CreateBookingRoomHandler(ApplicationDbContext context)
        : ICommandHandler<CreateBookingRoomCommand, CreateBookingRoomResult>
    {
        public async Task<CreateBookingRoomResult> Handle(CreateBookingRoomCommand command, CancellationToken cancellationToken)
        {
            foreach(var roomId in command.RoomIds)
            {
                var bookingroom = new BookingRoom
                {
                    BookingId = command.BookingId,
                    RoomId = roomId,
                };
                context.BookingRooms.Add(bookingroom);
            }
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookingRoomResult(true, command.RoomIds);

        }
    }
}
