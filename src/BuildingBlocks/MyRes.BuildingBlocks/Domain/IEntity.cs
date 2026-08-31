namespace MyRes.Shared.Domain
{
    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }

    public interface IEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}
