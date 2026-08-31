using MediatR;

namespace MyRes.BuildingBlocks.Application.CQRS
{
    public interface IQuery<out T> : IRequest<T> where T : notnull
    {
    }
}
