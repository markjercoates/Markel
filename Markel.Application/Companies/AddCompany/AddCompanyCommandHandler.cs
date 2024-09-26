using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Messaging;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using Markel.Application.Entities;

namespace Markel.Application.Companies.AddCompany;

public class AddCompanyCommandHandler : ICommandHandler<AddCompanyCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
   
    public AddCompanyCommandHandler(IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
    {
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
    }
    
    public async Task<Result<int>> Handle(AddCompanyCommand request, CancellationToken cancellationToken = default)
    {
        var company = new Company(0, request.Name, request.Address1, request.Address2, request.Address3,
            request.PostCode, request.Country, request.IsActive, request.InsuranceEndDate)
        {
            Name = request.Name,
        };
        
        _companyRepository.Add(company);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}