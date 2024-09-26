using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Markel.Application.Claims.GetClaim;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Claims.GetClaims;

public class GetAllClaimsQueryHandler : IQueryHandler<GetAllClaimsQuery, IReadOnlyList<ClaimResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllClaimsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<ClaimResponse>>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken = default)
    {
        List<ClaimResponse> claims = await _context.Claims.AsNoTracking()
            .Include(c => c.ClaimType)
            .Include(c => c.Company)
            .Select(c => new ClaimResponse
            {
                Id = c.Id,
                UCR = c.UCR,
                CompanyId = c.CompanyId,
                CompanyName = c.Company.Name,
                ClaimTypeName = c.ClaimType.Name,
                ClaimTypeId = c.ClaimTypeId,
                Closed = c.Closed,
                AssuredName = c.AssuredName,
                IncurredLoss = c.IncurredLoss,
                ClaimDate = c.ClaimDate,
                LossDate = c.LossDate,
                NumberOfDaysOld = c.NumberOfDays
            })
            .ToListAsync(cancellationToken);

        return claims;
    }
}