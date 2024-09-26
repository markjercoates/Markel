using Markel.Application.Abstractions.Results;
using MediatR;

namespace Markel.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
{
    
}