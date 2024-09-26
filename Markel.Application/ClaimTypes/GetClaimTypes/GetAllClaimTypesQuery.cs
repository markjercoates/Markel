using Markel.Application.Abstractions.Messaging;
using Markel.Application.Entities;

namespace Markel.Application.ClaimTypes.GetClaimTypes;

public record GetAllClaimTypesQuery : IQuery<IReadOnlyList<ClaimTypeResponse>>;