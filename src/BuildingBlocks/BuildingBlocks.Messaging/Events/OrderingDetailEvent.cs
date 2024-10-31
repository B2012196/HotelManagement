namespace BuildingBlocks.Messaging.Events
{
    public record OrderingDetailEvent : IntegrationEvent
    {
        public Guid OrderingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Numberofservice { get; set; }
    }
}
