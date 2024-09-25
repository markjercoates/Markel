using Markel.Application.Entities;

namespace Markel.Application.Abstractions.Repositories;

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(int id);
    
    void Add(Claim claim);
    
    void Update(Claim claim);
}