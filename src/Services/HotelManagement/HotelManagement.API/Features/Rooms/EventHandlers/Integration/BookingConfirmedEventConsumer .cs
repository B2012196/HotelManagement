namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingConfirmedEventConsumer(IRoomRepository repository)
        : IConsumer<BookingConfirmedEvent>
    {
        public async Task Consume(ConsumeContext<BookingConfirmedEvent> context)
        {
            var eventMessage = context.Message;

            foreach(var roomid in eventMessage.RoomIds)
            {
                await repository.UpdateRoomConfirmStatus(roomid, context.CancellationToken);
            }
            //return Task.CompletedTask;
        }
    }
}
