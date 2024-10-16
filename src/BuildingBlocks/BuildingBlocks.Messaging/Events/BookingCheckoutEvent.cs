namespace BuildingBlocks.Messaging.Events
{
    public record BookingCheckoutEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
    }
}
