//using BuildingBlocks.Messaging.Events;
//using MassTransit;

//namespace GuestManagement.API.Guests.EventHandlers.Integration
//{
//    public class GuestInfoRequestedConsumer(IGuestRepository repository)
//        : IConsumer<GuestInfoRequested>
//    {
//        public Task Consume(ConsumeContext<GuestInfoRequested> context)
//        {
//            var eventMessage = context.Message;
//            await repository.ge(eventMessage.RoomId, context.CancellationToken);
//        }
//    }
//}
