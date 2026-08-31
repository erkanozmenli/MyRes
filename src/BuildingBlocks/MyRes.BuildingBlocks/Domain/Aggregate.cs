namespace MyRes.Shared.Domain
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {

    }
}
