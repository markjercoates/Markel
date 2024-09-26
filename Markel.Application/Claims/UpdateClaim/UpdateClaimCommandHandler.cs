using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using Markel.Application.ClaimTypes;
using Markel.Application.Companies;
using Markel.Application.Entities;

namespace Markel.Application.Claims.UpdateClaim;

public class UpdateClaimCommandHandler : ICommandHandler<UpdateClaimCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimRepository _claimRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IClaimTypeRepository _claimTypeRepository;

    public UpdateClaimCommandHandler(IUnitOfWork unitOfWork, IClaimRepository claimRepository, 
        ICompanyRepository companyRepository, IClaimTypeRepository claimTypeRepository)
    {
        _unitOfWork = unitOfWork;
        _claimRepository = claimRepository;
        _companyRepository = companyRepository;
        _claimTypeRepository = claimTypeRepository;
    }
    
    public async Task<Result> Handle(UpdateClaimCommand request, CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId);
        if (company == null)
        {
            return Result.Failure<int>(CompanyErrors.NotFound);
        }
        
        var claimType = await _claimTypeRepository.GetByIdAsync(request.ClaimTypeId);
        if (claimType == null)
        {
            return Result.Failure<int>(ClaimTypeErrors.NotFound);
        }

        var claim = await _claimRepository.GetByIdAsync(request.Id);
        if (claim == null)
        {
            return Result.Failure(ClaimErrors.NotFound);
        }
        
        request.MapToClaim(claim);
        
        _claimRepository.Update(claim);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}