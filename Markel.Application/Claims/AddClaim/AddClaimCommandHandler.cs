using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using Markel.Application.ClaimTypes;
using Markel.Application.Companies;
using Markel.Application.Entities;

namespace Markel.Application.Claims.AddClaim;

public class AddClaimCommandHandler : ICommandHandler<AddClaimCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimRepository _claimRepository;
  
    public AddClaimCommandHandler(IUnitOfWork unitOfWork, IClaimRepository claimRepository)
    {
        _unitOfWork = unitOfWork;
        _claimRepository = claimRepository;      
    }
    
    public async Task<Result<int>> Handle(AddClaimCommand request, CancellationToken cancellationToken = default)
    {
        var claim = new Claim(0, request.ClaimTypeId, request.UCR, request.ClaimDate, request.LossDate,
            request.AssuredName, request.IncurredLoss, request.Closed, request.CompanyId)
        {
            UCR = request.UCR,
            AssuredName = request.AssuredName,
        };
        
        _claimRepository.Add(claim);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return claim.Id;
    }
}