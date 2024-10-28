namespace BuildingBlocks.Messaging.Events
{
    public record CreateStaffEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
