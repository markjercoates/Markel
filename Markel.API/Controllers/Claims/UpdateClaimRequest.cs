namespace Markel.API.Controllers.Claims;

public record UpdateClaimRequest
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