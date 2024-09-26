using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Markel.Application.ClaimTypes;
using Markel.Application.ClaimTypes.GetClaimType;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Claims.GetClaim;

public class GetClaimTypeQueryHandler : IQueryHandler<GetClaimTypeQuery, ClaimTypeResponse>
{
    private readonly IApplicationDbContext _context;

    public GetClaimTypeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ClaimTypeResponse>> Handle(GetClaimTypeQuery request, CancellationToken cancellationToken = default)
    {
        ClaimTypeResponse? claimType = await _context.ClaimTypes.AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(c => new ClaimTypeResponse
            {
                Id = c.Id,
                Name = c.Name,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (claimType == null)
        {
            return Result.Failure<ClaimTypeResponse>(ClaimTypeErrors.NotFound);    
        }

        return claimType;
    }
}