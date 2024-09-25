using Markel.Application.Entities;

namespace Markel.Application.Abstractions.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(int id);
    
    void Add(Company company);
    
    void Update(Company company);
}