using Asp.Versioning;
using Markel.Application.Abstractions.Results;
using Markel.Application.Companies.GetCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Markel.API.Controllers.Claims;

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
    public async Task<IActionResult> GetClaim(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetCompanyQuery(id);

        Result<CompanyResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
}