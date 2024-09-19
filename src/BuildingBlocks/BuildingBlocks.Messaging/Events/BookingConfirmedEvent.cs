namespace BuildingBlocks.Messaging.Events
{
    public record BookingConfirmedEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }

    }
}
