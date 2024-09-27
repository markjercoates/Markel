using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Claims.GetClaims;

public class GetAllClaimsByCompanyQueryHandler : IQueryHandler<GetAllClaimsByCompanyQuery, IReadOnlyList<ClaimResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllClaimsByCompanyQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<ClaimResponse>>> Handle(GetAllClaimsByCompanyQuery request, CancellationToken cancellationToken = default)
    {
        List<ClaimResponse> claims = await _context.Claims.AsNoTracking()
            .Where(c => c.CompanyId == request.CompanyId)
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