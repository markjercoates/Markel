using Markel.Application.Entities;

namespace Markel.Application.Abstractions.Repositories;

public interface IClaimTypeRepository
{
    Task<ClaimType?> GetByIdAsync(int id);
    
    void Add(ClaimType claimType);
    
    void Update(ClaimType claimType);
}