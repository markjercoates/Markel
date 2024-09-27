using Markel.Application.Claims;

namespace Markel.Application.Companies;

public class CompanyClaimsResponse
{
    public int Id { get; init; }
    public required string Name { get; init; } 
    public string Address1 { get; init; } = string.Empty;
    public string Address2 { get; init; } = string.Empty;
    public string Address3 { get; init; } = string.Empty;
    public string PostCode { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public bool Active { get; init; }
    public DateTime InsuranceEndDate { get; init; }
    public bool HasActivePolicy { get; init; }
    public IEnumerable<ClaimResponse> Claims { get; set; } = new List<ClaimResponse>();
}