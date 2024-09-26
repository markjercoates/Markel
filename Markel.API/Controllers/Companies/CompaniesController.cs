using Asp.Versioning;
using Markel.Application.Abstractions.Results;
using Markel.Application.Companies;
using Markel.Application.Companies.AddCompany;
using Markel.Application.Companies.GetCompanies;
using Markel.Application.Companies.GetCompany;
using Markel.Application.Companies.UpdateCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Markel.API.Controllers.Companies;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ISender _sender;
    
    public CompaniesController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompany(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetCompanyQuery(id);

        Result<CompanyResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken = default)
    {
        var query = new GetAllCompaniesQuery();

        Result<IReadOnlyList<CompanyResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCompany(AddCompanyRequest request, CancellationToken cancellationToken = default)
    {
        var command = new AddCompanyCommand(request.Name, request.Address1, request.Address2, request.Address3,
            request.PostCode, request.Country, request.IsActive, request.InsuranceEndDate);

        Result<int> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetCompany), new { id = result.Value }, result.Value);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyRequest request, CancellationToken cancellationToken = default)
    {
        var command = new UpdateCompanyCommand(id, request.Name, request.Address1, request.Address2, request.Address3,
            request.PostCode, request.Country, request.IsActive, request.InsuranceEndDate);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}