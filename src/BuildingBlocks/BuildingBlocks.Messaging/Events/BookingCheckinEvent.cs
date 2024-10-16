namespace BuildingBlocks.Messaging.Events
{
    public record BookingCheckinEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
    }
}
