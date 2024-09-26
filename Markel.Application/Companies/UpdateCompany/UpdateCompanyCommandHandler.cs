using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;

namespace Markel.Application.Companies.UpdateCompany;

public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
   
    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
    {
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
    }
    
    public async Task<Result> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken = default)
    {
        var company = await _companyRepository.GetByIdAsync(request.Id);
        if (company == null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }
        
        request.MapToCompany(company);
        
        _companyRepository.Update(company);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}