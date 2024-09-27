using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Claims.GetClaims;

public record GetAllClaimsByCompanyQuery(int CompanyId) : IQuery<IReadOnlyList<ClaimResponse>>;