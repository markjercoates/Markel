using Markel.Application.Abstractions.Errors;

namespace Markel.Application.Companies;

public static class CompanyErrors
{
    public static readonly Error NotFound = new Error("Company.NotFound", "Company with specified identifier was not found");
    public static Error CustomNotFound(string message) => new Error("Company.NotFound", message);
}