using MediatR;

namespace Survey.Application.Features
{
    public class BaseRequest<T> : IRequest<T>
    {
    }
}