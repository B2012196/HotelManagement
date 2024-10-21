namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingCheckoutEventConsumer(IRoomRepository repository) : IConsumer<BookingCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BookingCheckoutEvent> context)
        {
            var eventMessage = context.Message;
            await repository.UpdateRoomCheckoutStatus(eventMessage.RoomId, context.CancellationToken);
        }
    }
}
