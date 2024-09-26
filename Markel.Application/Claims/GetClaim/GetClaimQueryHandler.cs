using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Claims.GetClaim;

public class GetClaimQueryHandler : IQueryHandler<GetClaimQuery, ClaimResponse>
{
    private readonly IApplicationDbContext _context;

    public GetClaimQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ClaimResponse>> Handle(GetClaimQuery request, CancellationToken cancellationToken = default)
    {
        ClaimResponse? claimResponse = await _context.Claims.AsNoTracking()
            .Include(c => c.ClaimType)
            .Include(c => c.Company)
            .Where(c => c.Id == request.Id)
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
            .SingleOrDefaultAsync(cancellationToken);

        if (claimResponse == null)
        {
            return Result.Failure<ClaimResponse>(ClaimErrors.NotFound);    
        }

        return claimResponse;
    }
}