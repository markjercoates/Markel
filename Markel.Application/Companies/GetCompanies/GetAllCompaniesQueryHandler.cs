using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Markel.Application.Companies.GetCompanies;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Companies.GetAllCompanies;

public class GetAllCompaniesQueryHandler : IQueryHandler<GetAllCompaniesQuery, IReadOnlyList<CompanyResponse>>
{
    private readonly IApplicationDbContext _context;
    
    public GetAllCompaniesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IReadOnlyList<CompanyResponse>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken = default)
    {
        List<CompanyResponse> companies = await _context.Companies
            .Select(c => new CompanyResponse
            {
                Id = c.Id,
                Name = c.Name,
                Address1 = c.Address1,
                Address2 = c.Address2,
                Address3 = c.Address3, 
                PostCode = c.PostCode,
                Country = c.Country,
                Active = c.Active,
                InsuranceEndDate = c.InsuranceEndDate,
            })
            .ToListAsync(cancellationToken);

        return companies;
    }
}