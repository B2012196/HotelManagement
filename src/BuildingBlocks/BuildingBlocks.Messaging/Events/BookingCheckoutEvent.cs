namespace BuildingBlocks.Messaging.Events
{
    public record BookingCheckoutEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public List<Guid> RoomIds { get; set; }
    }
}
