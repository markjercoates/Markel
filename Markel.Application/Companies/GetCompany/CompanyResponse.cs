namespace Markel.Application.Companies.GetCompany;

public class CompanyResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string Address1 { get; init; } = null!;
    public string Address2 { get; init; } = null!;
    public string Address3 { get; init; } = null!;
    public string PostCode { get; init; } = null!;
    public string Country { get; init; } = null!;
    public bool Active { get; init; }
    public DateTime InsuranceEndDate { get; init; }
}