﻿using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Results;
using Microsoft.EntityFrameworkCore;

namespace Markel.Application.Companies.GetCompany;

public class GetCompanyQueryHandler : IQueryHandler<GetCompanyQuery, CompanyResponse>
{
    private readonly IApplicationDbContext _context;
    
    public GetCompanyQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CompanyResponse>> Handle(GetCompanyQuery request, CancellationToken cancellationToken = default)
    {
        CompanyResponse? companyResponse = await _context.Companies
            .Where(c => c.Id == request.Id)
            .Select(c => new CompanyResponse
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
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (companyResponse is null)
        {
            return Result.Failure<CompanyResponse>(CompanyErrors.NotFound);
        }
        
        return companyResponse;
    }
}