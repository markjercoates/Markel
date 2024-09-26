namespace Markel.Application.Claims;

public class ClaimResponse
{
    public int Id { get; init; }
    public int ClaimTypeId { get; init; }
    public string ClaimTypeName { get; init; } = string.Empty;
    public required string UCR { get; init; } 
    public DateTime ClaimDate { get; init; } 
    public DateTime LossDate { get; init; }
    public required string AssuredName { get; init; }
    public decimal IncurredLoss { get; init; }
    public bool Closed { get; init; }
    public int CompanyId { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public int NumberOfDaysOld { get; init; }
}