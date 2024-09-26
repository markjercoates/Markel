using Markel.Application.Abstractions.Messaging;
using Markel.Application.Entities;

namespace Markel.Application.Companies.GetCompanies;

public record GetAllCompaniesQuery() : IQuery<IReadOnlyList<CompanyResponse>>;