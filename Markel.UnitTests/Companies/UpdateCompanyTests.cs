using Markel.Application.Companies.UpdateCompany;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using Markel.Application.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Markel.UnitTests.Companies;

public class UpdateCompanyTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly UpdateCompanyCommand Command = new UpdateCompanyCommand(1,"CompanyName", "Address1",
        "Address2", "Address3", "PostCode", 
        "Country", true, UtcNow.AddDays(7));
    
    private readonly ICompanyRepository _companyRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly UpdateCompanyCommandHandler _updateCompanyCommandHandler;
   
    public UpdateCompanyTests()
    {
        _companyRepositoryMock = Substitute.For<ICompanyRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _updateCompanyCommandHandler = new UpdateCompanyCommandHandler(_unitOfWorkMock, _companyRepositoryMock);
    }

    [Fact]
    public async Task UpdateCompanyHandler_Should_ReturnFailure_When_CompanyId_NotFound()
    {
        // Arrange
        _companyRepositoryMock.GetByIdAsync(Command.Id).ReturnsNull();
        
        // Act
        var result = await _updateCompanyCommandHandler.Handle(Command, default);
        
        // Assert
        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public async Task UpdateCompanyHandler_Should_ReturnSuccess_When_Update_Succeeds()
    {
        // Arrange
        var company = new Company() { Name = "CompanyName"};
       
        _unitOfWorkMock.SaveChangesAsync().Returns(1);
        _companyRepositoryMock.GetByIdAsync(Command.Id).Returns(company);
        
        // Act
        var result = await _updateCompanyCommandHandler.Handle(Command, default);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}