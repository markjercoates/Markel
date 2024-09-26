using Markel.Application.Claims.UpdateClaim;
using Markel.Application.Entities;

namespace Markel.Application.Claims;

public static class ClaimExtensions
{
     public static void MapToClaim(this UpdateClaimCommand command, Claim claim)
     {
          claim.UCR = command.UCR;
          claim.ClaimTypeId = command.ClaimTypeId;
          claim.CompanyId = command.CompanyId;
          claim.ClaimDate = command.ClaimDate;
          claim.LossDate = command.LossDate;
          claim.Closed = command.Closed;
          claim.AssuredName = command.AssuredName;
          claim.IncurredLoss = command.IncurredLoss;
     }
}