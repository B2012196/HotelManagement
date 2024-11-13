namespace BuildingBlocks.Messaging.Events
{
    public record BookingCheckinEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public List<Guid> RoomIds { get; set; }
    }
}
