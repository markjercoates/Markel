using Markel.Application.Companies.UpdateCompany;
using Markel.Application.Entities;

namespace Markel.Application.Companies;

public static class CompanyExtensions
{
    public static void MapToCompany(this UpdateCompanyCommand command, Company company)
    {
        company.Name = command.Name;
        company.Address1 = command.Address1;
        company.Address2 = command.Address2;
        company.Address3 = command.Address3;
        company.PostCode = command.PostCode;
        company.Country = command.Country;
        company.Active = command.IsActive;
        company.InsuranceEndDate = command.InsuranceEndDate; 
    }
}