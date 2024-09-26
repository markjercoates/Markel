using Asp.Versioning;
using Markel.Application.Abstractions.Results;
using Markel.Application.ClaimTypes;
using Markel.Application.ClaimTypes.GetClaimType;
using Markel.Application.ClaimTypes.GetClaimTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Markel.API.Controllers.ClaimTypes;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/[controller]")]
public class ClaimTypesController : ControllerBase
{
    private readonly ISender _sender;

    public ClaimTypesController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClaimType(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetClaimTypeQuery(id);

        Result<ClaimTypeResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetClaimTypes(CancellationToken cancellationToken = default)
    {
        var query = new GetAllClaimTypesQuery();

        Result<IReadOnlyList<ClaimTypeResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
}