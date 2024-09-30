using Markel.Application.Companies.GetCompany;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Results;
using Markel.Application.Companies;
using Markel.Application.Entities;
using NSubstitute;
using MockQueryable.NSubstitute;
using Microsoft.EntityFrameworkCore;

namespace Markel.UnitTests.Companies;

public class GetCompanyWithClaimsTests
{
    private readonly IApplicationDbContext _dbContextMock;
    private readonly GetCompanyClaimsQueryHandler _getCompanyClaimsQueryHandler;
    private static readonly GetCompanyClaimsQuery CompanyClaimsQuery = new GetCompanyClaimsQuery(1);

    public GetCompanyWithClaimsTests()
    {
        _dbContextMock = Substitute.For<IApplicationDbContext>();
        _getCompanyClaimsQueryHandler = new GetCompanyClaimsQueryHandler(_dbContextMock);
    }

    [Fact]
    public async Task GetCompanyClaimsQueryHandle_Should_Return_Failure_When_Not_Found()
    {
        // Arrange
        var list = new List<Company>();

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyClaimsResponse> result = await _getCompanyClaimsQueryHandler.Handle(CompanyClaimsQuery);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task GetCompanyClaimsQueryHandle_Should_Return_Success_When_Found()
    {
        // Arrange
        var claimTypes = new List<ClaimType>
        {
            new ClaimType
            {
                Id = 1,
                Name = "Claim Type 1"
            }
        };

        var claims = new List<Claim>
        {
            new Claim
            {
                Id = 1,
                CompanyId = 1,
                ClaimTypeId = 1,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Now.AddDays(-1),
                AssuredName = "Assured Name",
                UCR = "UCR",
                Closed = false,
                IncurredLoss = 1000,
            }
        };

        var companies = new List<Company>
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
                Claims = new List<Claim>
                {
                    new Claim
                    {
                        Id = 1,
                        CompanyId = 1,
                        ClaimTypeId = 1,
                        ClaimDate = DateTime.Now,
                        LossDate = DateTime.Now.AddDays(-1),
                        AssuredName = "Assured Name",
                        UCR = "UCR",
                        Closed = false,
                        IncurredLoss = 1000,
                        ClaimType = claimTypes[0]
                    }
                }
            }
        };        

        var mock = companies.AsQueryable().BuildMockDbSet();
         mock.Include(c => claims).ThenInclude(claim => claimTypes);
        _dbContextMock.Companies.Returns(mock);

        // Act
        Result<CompanyClaimsResponse> result = await _getCompanyClaimsQueryHandler.Handle(CompanyClaimsQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(1);
        result.Value.Name.Should().Be("Company 1");
        result.Value.Claims.Should().HaveCount(1);
        result.Value.Claims.ToList()[0].Id.Should().Be(1);
        result.Value.Claims.ToList()[0].UCR.Should().Be("UCR");
        result.Value.Claims.ToList()[0].CompanyId.Should().Be(1);
    }    
}
