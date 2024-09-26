using Markel.Application.Abstractions.Messaging;
using Markel.Application.Entities;

namespace Markel.Application.Companies.GetCompany;

public record GetCompanyQuery(int Id) : IQuery<CompanyResponse>;