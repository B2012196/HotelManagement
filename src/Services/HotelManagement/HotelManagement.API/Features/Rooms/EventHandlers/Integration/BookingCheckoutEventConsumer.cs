namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingCheckoutEventConsumer(IRoomRepository repository) : IConsumer<BookingCheckinEvent>
    {
        public async Task Consume(ConsumeContext<BookingCheckinEvent> context)
        {
            var eventMessage = context.Message;
            await repository.UpdateRoomCheckoutStatus(eventMessage.RoomId, context.CancellationToken);
        }
    }
}
