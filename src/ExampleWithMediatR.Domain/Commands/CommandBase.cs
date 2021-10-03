using MediatR;

namespace ExampleWithMediatR.Domain.Commands
{
    public abstract class CommandBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}
