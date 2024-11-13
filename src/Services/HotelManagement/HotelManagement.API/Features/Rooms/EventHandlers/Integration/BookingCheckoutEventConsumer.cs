using HotelManagement.API.Features.Rooms.Queries.GetRoomById;

namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingCheckoutEventConsumer(IRoomRepository repository) : IConsumer<BookingCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BookingCheckoutEvent> context)
        {
            var eventMessage = context.Message;
            foreach (var roomid in eventMessage.RoomIds)
            {
                await repository.UpdateRoomCheckoutStatus(roomid, context.CancellationToken);
            }
        }
    }
}
