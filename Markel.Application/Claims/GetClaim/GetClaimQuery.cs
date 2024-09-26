using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Claims.GetClaim;

public record GetClaimQuery(int Id) : IQuery<ClaimResponse>;
