using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Claims.AddClaim;

public record AddClaimCommand
(
    string UCR,
    int ClaimTypeId,
    int CompanyId,
    DateTime ClaimDate,
    DateTime LossDate,
    string AssuredName,
    decimal IncurredLoss,
    bool Closed
) : ICommand<int>;