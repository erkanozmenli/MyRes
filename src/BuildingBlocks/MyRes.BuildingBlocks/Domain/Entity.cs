namespace MyRes.Shared.Domain
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}
