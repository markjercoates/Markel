using Markel.Application.Models;
using MediatR;

namespace Markel.Application.Abstractions.Messaging;

// TResponse is the type wrapped by a Result type
// The query is a MediatR Request returning a TResponse
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}