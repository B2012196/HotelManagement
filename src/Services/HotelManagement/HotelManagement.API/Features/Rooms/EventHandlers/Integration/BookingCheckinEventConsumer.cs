
namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingCheckinEventConsumer(IRoomRepository repository) : IConsumer<BookingCheckinEvent>
    {
        public async Task Consume(ConsumeContext<BookingCheckinEvent> context)
        {
            var eventMessage = context.Message;
            await repository.UpdateRoomCheckinStatus(eventMessage.RoomId, context.CancellationToken);
        }
    }
}
