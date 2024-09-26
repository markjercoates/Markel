using Markel.Application.Abstractions.Messaging;


namespace Markel.Application.Claims.GetClaims;

public record GetAllClaimsQuery : IQuery<IReadOnlyList<ClaimResponse>>;
