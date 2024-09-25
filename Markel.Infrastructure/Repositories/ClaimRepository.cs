using Markel.Application.Abstractions.Repositories;
using Markel.Application.Entities;
using Markel.Infrastructure.Data;

namespace Markel.Infrastructure.Repositories;

public class ClaimRepository : GenericRepository<Claim>, IClaimRepository
{
    public ClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}