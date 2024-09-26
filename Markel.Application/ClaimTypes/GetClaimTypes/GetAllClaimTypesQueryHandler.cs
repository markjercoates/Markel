using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Markel.Application.Claims.GetClaim;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.ClaimTypes.GetClaimTypes;

public class GetAllClaimTypesQueryHandler : IQueryHandler<GetAllClaimTypesQuery, IReadOnlyList<ClaimTypeResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllClaimTypesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<ClaimTypeResponse>>> Handle(GetAllClaimTypesQuery request, CancellationToken cancellationToken = default)
    {
        List<ClaimTypeResponse> claimTypes = await _context.ClaimTypes.AsNoTracking()
            .Select(c => new ClaimTypeResponse
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync(cancellationToken);

        return claimTypes;
    }
}