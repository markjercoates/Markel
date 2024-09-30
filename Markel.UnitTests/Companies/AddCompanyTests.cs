using Markel.Application.Companies.AddCompany;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using NSubstitute;

namespace Markel.UnitTests.Companies;

public class AddCompanyTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly AddCompanyCommand Command = new AddCompanyCommand("CompanyName", "Address1",
        "Address2", "Address3", "PostCode", 
        "Country", true, UtcNow.AddDays(7));
    
    private readonly ICompanyRepository _companyRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly AddCompanyCommandHandler _addCompanyCommandHandler;
   
    public AddCompanyTests()
    {
        _companyRepositoryMock = Substitute.For<ICompanyRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _addCompanyCommandHandler = new AddCompanyCommandHandler(_unitOfWorkMock, _companyRepositoryMock);
    }

    [Fact]
    public async Task AddCompanyHandler_Should_ReturnSuccess()
    {
        // Arrange
        _unitOfWorkMock.SaveChangesAsync().Returns(1);
        
        // Act
        Result<int> result = await _addCompanyCommandHandler.Handle(Command, default);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}