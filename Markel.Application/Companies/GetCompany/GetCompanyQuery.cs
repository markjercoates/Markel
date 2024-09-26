using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Companies.GetCompany;

public record GetCompanyQuery(int Id) : IQuery<CompanyResponse>;