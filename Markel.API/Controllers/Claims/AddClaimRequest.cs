namespace Markel.API.Controllers.Claims;

public record AddClaimRequest
(
    string UCR,
    int ClaimTypeId,
    int CompanyId,
    DateTime ClaimDate,
    DateTime LossDate,
    string AssuredName,
    decimal IncurredLoss,
    bool Closed
);