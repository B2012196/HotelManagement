namespace GuestManagement.API.Guests.EventHandlers.Integration
{
    public class CreateGuestConsumer(IGuestRepository repository) : IConsumer<CreateGuestEvent>
    {
        public async Task Consume(ConsumeContext<CreateGuestEvent> context)
        {
            var eventMessage = context.Message;

            var guest = new Guest
            {
                GuestId = Guid.NewGuid(),
                UserId = eventMessage.UserId,
                FirstName = string.Empty,
                LastName = string.Empty,
                DateofBirst = DateTime.MinValue,
                Address = string.Empty,
            };

            await repository.CreateGuest(guest, context.CancellationToken);
        }
    }
}
