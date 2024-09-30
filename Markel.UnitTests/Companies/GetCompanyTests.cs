using Markel.Application.Companies.GetCompany;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Results;
using Markel.Application.Companies;
using Markel.Application.Entities;
using NSubstitute;
using MockQueryable.NSubstitute;


namespace Markel.UnitTests.Companies;

public class GetCompanyTests
{
    private readonly IApplicationDbContext _dbContextMock;
    private readonly GetCompanyQueryHandler _getCompanyQueryHandler;
    private static readonly GetCompanyQuery CompanyQuery = new GetCompanyQuery(1);

    public GetCompanyTests()
    {
        _dbContextMock = Substitute.For<IApplicationDbContext>();
        _getCompanyQueryHandler = new GetCompanyQueryHandler(_dbContextMock);
    }

    [Fact]
    public async Task GeCompanyQueryHandle_Should_Return_Failure_When_Not_Found()
    {
        // Arrange
        var list = new List<Company>();

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyResponse> result = await _getCompanyQueryHandler.Handle(CompanyQuery);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task GetCompanyQueryHandle_Should_Return_Success_When_Found()
    {
        // Arrange
        var list = new List<Company>
        {
            new Company {
                Id = 1,
                Name = "Company 1",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(7),               
            }
        };

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyResponse> result = await _getCompanyQueryHandler.Handle(CompanyQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(1);
        result.Value.Name.Should().Be("Company 1");
    }

    [Fact]
    public async Task GetCompanyQueryHandle_Should_Return_HasActivePolicy_When_CompanyActive_And_InsuranceEndDate_NotExpired()
    {
        // Arrange
        var list = new List<Company>
        {
            new Company {
                Id = 1,
                Name = "Company 1",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(7),
            }
        };

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyResponse> result = await _getCompanyQueryHandler.Handle(CompanyQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.HasActivePolicy.Should().BeTrue();        
    }

    [Fact]
    public async Task GetCompanyQueryHandle_Should_Return_HasNotActivePolicy_When_CompanyActive_And_InsuranceEndDate_Expired()
    {
        // Arrange
        var list = new List<Company>
        {
            new Company {
                Id = 1,
                Name = "Company 1",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(-1),
            }
        };

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyResponse> result = await _getCompanyQueryHandler.Handle(CompanyQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.HasActivePolicy.Should().BeFalse();
    }

    [Fact]
    public async Task GetCompanyQueryHandle_Should_Return_HasNotActivePolicy_When_CompanyNotActive_And_InsuranceEndDate_NotExpired()
    {
        // Arrange
        var list = new List<Company>
        {
            new Company {
                Id = 1,
                Name = "Company 1",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = false,
                InsuranceEndDate = DateTime.Now.AddDays(1),
            }
        };

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyResponse> result = await _getCompanyQueryHandler.Handle(CompanyQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.HasActivePolicy.Should().BeFalse();
    }  

}
