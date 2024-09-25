using Markel.Application.Abstractions.Repositories;
using Markel.Application.Entities;
using Markel.Infrastructure.Data;

namespace Markel.Infrastructure.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext)
    {
        
    }
}