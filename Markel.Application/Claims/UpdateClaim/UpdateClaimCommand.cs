using Markel.Application.Abstractions.Messaging;

namespace Markel.Application.Claims.UpdateClaim;

public record UpdateClaimCommand(
    int Id,
    string UCR,
    int ClaimTypeId,
    int CompanyId,
    DateTime ClaimDate,
    DateTime LossDate,
    string AssuredName,
    decimal IncurredLoss,
    bool Closed
) : ICommand;