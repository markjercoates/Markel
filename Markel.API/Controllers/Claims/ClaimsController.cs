using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Markel.Application.Abstractions.Results;
using Markel.Application.Claims.GetClaim;
using MediatR;

namespace Markel.API.Controllers.Claims;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly ISender _sender;
    
    public ClaimsController(ISender sender)
    {
        _sender = sender;
    }
   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClaim(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetClaimQuery(id);

        Result<ClaimResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
   
}