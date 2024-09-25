using Markel.Application.Abstractions.Repositories;
using Markel.Application.Entities;
using Markel.Infrastructure.Data;

namespace Markel.Infrastructure.Repositories;

public class ClaimTypeRepository : GenericRepository<ClaimType>, IClaimTypeRepository
{
    public ClaimTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}   