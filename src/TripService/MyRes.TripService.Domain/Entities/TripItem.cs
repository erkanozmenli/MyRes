using MyRes.Shared.Domain;

namespace MyRes.TripService.Domain.Entities
{
    public abstract class TripItem : Entity<int>
    {
        public Guid TripId { get; protected set; }

        protected TripItem() { }
    }
}
