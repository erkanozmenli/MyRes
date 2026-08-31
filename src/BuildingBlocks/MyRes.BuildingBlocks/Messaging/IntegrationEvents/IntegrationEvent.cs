namespace MyRes.BuildingBlocks.Messaging.IntegrationEvents
{
    public abstract record IntegrationEvent
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    }
}
