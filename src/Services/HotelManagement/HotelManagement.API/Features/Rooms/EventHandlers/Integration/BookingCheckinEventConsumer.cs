
namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingCheckinEventConsumer(IRoomRepository repository) : IConsumer<BookingCheckinEvent>
    {
        public async Task Consume(ConsumeContext<BookingCheckinEvent> context)
        {
            var eventMessage = context.Message;
            foreach(var roomid in eventMessage.RoomIds)
            {
                await repository.UpdateRoomCheckinStatus(roomid, context.CancellationToken);
            }
        }
    }
}
