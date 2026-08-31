namespace MyRes.Shared.Domain
{
    public interface IAggregate<TId> : IAggregate, IEntity<TId>
    {

    }

    public interface IAggregate : IEntity
    {

    }
}
