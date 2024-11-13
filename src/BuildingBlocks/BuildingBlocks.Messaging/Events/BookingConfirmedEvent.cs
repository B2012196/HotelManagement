namespace BuildingBlocks.Messaging.Events
{
    public record BookingConfirmedEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public List<Guid> RoomIds { get; set; }
    }
}
