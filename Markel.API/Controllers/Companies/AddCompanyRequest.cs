namespace Markel.API.Controllers.Companies;

public record AddCompanyRequest(
    string Name,
    string Address1,
    string Address2,
    string Address3,
    string PostCode,
    string Country,
    bool IsActive,
    DateTime InsuranceEndDate
);