using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Companies.UpdateCompany;

public record UpdateCompanyCommand(
    int Id,
    string Name,
    string Address1,
    string Address2,
    string Address3,
    string PostCode,
    string Country,
    bool IsActive,
    DateTime InsuranceEndDate
) : ICommand;
