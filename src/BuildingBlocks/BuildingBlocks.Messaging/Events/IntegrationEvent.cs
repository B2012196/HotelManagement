namespace BuildingBlocks.Messaging.Events
{
    public record IntegrationEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
