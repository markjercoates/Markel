using Markel.Application.Abstractions.Errors;

namespace Markel.Application.ClaimTypes;

public static class ClaimTypeErrors
{
    public static readonly Error NotFound = new Error("ClaimType.NotFound", "ClaimType with specified identifier was not found");
}