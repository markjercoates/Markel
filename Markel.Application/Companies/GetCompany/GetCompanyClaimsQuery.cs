using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Companies.GetCompany;

public record GetCompanyClaimsQuery(int Id) : IQuery<CompanyClaimsResponse>;
