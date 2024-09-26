using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.ClaimTypes.GetClaimType;

public record GetClaimTypeQuery(int Id) : IQuery<ClaimTypeResponse>;
