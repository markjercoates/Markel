using Markel.Application.Abstractions.Results;
using MediatR;

namespace Markel.Application.Abstractions.Messaging;

// command returns just a result
public interface ICommand : IRequest<Result>, IBaseCommand
{
}

// command returns Response wrapped in a Result
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}