using Markel.Application.Abstractions.Errors;

namespace Markel.Application.Claims;

public static class ClaimErrors
{
    public static readonly Error NotFound = new Error("Claim.NotFound", "Claim with specified identifier was not found");
}