namespace BuildingBlocks.Messaging.Events
{
    public record CreateGuestEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
