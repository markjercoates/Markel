using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Markel.Application.Abstractions.Results;
using Markel.Application.Claims;
using Markel.Application.Claims.AddClaim;
using Markel.Application.Claims.GetClaim;
using Markel.Application.Claims.GetClaims;
using Markel.Application.Claims.UpdateClaim;
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
    
    [HttpGet]
    public async Task<IActionResult> GetClaims(CancellationToken cancellationToken = default)
    {
        var query = new GetAllClaimsQuery();

        Result<IReadOnlyList<ClaimResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddClaim(AddClaimRequest request, CancellationToken cancellationToken = default)
    {
        var command = new AddClaimCommand(request.UCR, request.ClaimTypeId, request.CompanyId, request.ClaimDate,
            request.LossDate, request.AssuredName, request.IncurredLoss, request.Closed);

        Result<int> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetClaim), new { id = result.Value }, result.Value);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClaim(int id, UpdateClaimRequest request, CancellationToken cancellationToken = default)
    {
        var command = new UpdateClaimCommand(id, request.UCR, request.ClaimTypeId, request.CompanyId, request.ClaimDate,
            request.LossDate, request.AssuredName, request.IncurredLoss, request.Closed);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok();
    }
}