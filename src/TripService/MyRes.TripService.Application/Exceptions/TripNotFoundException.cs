using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Application.Exceptions
{
    public class TripNotFoundException : NotFoundException
    {
        public TripNotFoundException(Guid id) : base("Trip", id)
        {

        }
    }
}
