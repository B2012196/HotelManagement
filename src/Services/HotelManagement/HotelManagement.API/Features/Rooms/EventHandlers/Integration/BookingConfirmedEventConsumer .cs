namespace HotelManagement.API.Features.Rooms.EventHandlers.Integration
{
    public class BookingConfirmedEventConsumer(IRoomRepository repository)
        : IConsumer<BookingConfirmedEvent>
    {
        public async Task Consume(ConsumeContext<BookingConfirmedEvent> context)
        {
            var eventMessage = context.Message;

            await repository.UpdateRoomConfirmStatus(eventMessage.RoomId, context.CancellationToken);
            //return Task.CompletedTask;
        }
    }
}
