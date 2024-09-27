using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Markel.Application.Claims;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Companies.GetCompany;

public class GetCompanyClaimsQueryHandler : IQueryHandler<GetCompanyClaimsQuery, CompanyClaimsResponse>
{
    private readonly IApplicationDbContext _context;
    
    public GetCompanyClaimsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CompanyClaimsResponse>> Handle(GetCompanyClaimsQuery request, CancellationToken cancellationToken = default)
    {
        CompanyClaimsResponse? companyResponse = await _context.Companies.AsNoTracking()
            .Include(c => c.Claims)
            .ThenInclude(claim => claim.ClaimType)
            .Where(c => c.Id == request.Id)
            .Select(c => new CompanyClaimsResponse
            {
                Id = c.Id,
                Name = c.Name,
                Address1 = c.Address1!,
                Address2 = c.Address2!,
                Address3 = c.Address3!, 
                PostCode = c.PostCode!,
                Country = c.Country!,
                Active = c.Active,
                InsuranceEndDate = c.InsuranceEndDate,
                HasActivePolicy = c.HasActivePolicy,
                Claims = c.Claims.Select(cl => new ClaimResponse
                {
                    Id = cl.Id,
                    UCR = cl.UCR,
                    CompanyId = cl.CompanyId,
                    ClaimTypeId = cl.ClaimTypeId,
                    ClaimTypeName = cl.ClaimType.Name,
                    AssuredName = cl.AssuredName,
                    ClaimDate = cl.ClaimDate,
                    LossDate = cl.LossDate,
                    Closed = cl.Closed,
                    IncurredLoss = cl.IncurredLoss,
                    NumberOfDaysOld = cl.NumberOfDays
                }).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (companyResponse is null)
        {
            return Result.Failure<CompanyClaimsResponse>(CompanyErrors
                .CustomNotFound($"Company with specified identifier {request.Id} was not found"));
        }
        
        return companyResponse;
    }
}