using Markel.Application.Claims.GetClaim;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Results;
using Markel.Application.Claims;
using Markel.Application.Entities;
using NSubstitute;
using MockQueryable.NSubstitute;
using Microsoft.EntityFrameworkCore;

namespace Markel.UnitTests.Claims;
public class GetClaimTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private readonly IApplicationDbContext _dbContextMock;
    private readonly GetClaimQueryHandler _getClaimQueryHandler;
    private static readonly GetClaimQuery ClaimQuery = new GetClaimQuery(1);

    public GetClaimTests()
    {
        _dbContextMock = Substitute.For<IApplicationDbContext>();
        _getClaimQueryHandler = new GetClaimQueryHandler(_dbContextMock);
    }

    [Fact]
    public async Task GetClaimQueryHandle_Should_Return_Failure_When_Not_Found()
    {
        // Arrange
        var list = new List<Claim>();

        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Claims.Returns(mock);

        // Act
        Result<ClaimResponse> result = await _getClaimQueryHandler.Handle(ClaimQuery);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task GetClaimQueryHandle_Should_Return_Success_With_NumberDaysOld_When_Found()
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

        var companies = new List<Company>
        {
            new Company
            {
                Id = 1,
                Name = "Company 1",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = true,
                InsuranceEndDate = UtcNow.AddDays(7),
            }
        };

        var list = new List<Claim>
        { 
            new Claim
            {
                Id = 1,
                UCR = "UCR",
                AssuredName = "AssuredName",
                CompanyId = 1,
                ClaimTypeId = 1,
                LossDate = UtcNow.AddDays(-14),
                ClaimDate = UtcNow.AddDays(-7),
                IncurredLoss = 1000,
                Closed = false,
                ClaimType = claimTypes.First(),
                Company = companies.First()
            }
        };
       
        var mock = list.AsQueryable().BuildMockDbSet();
        _dbContextMock.Claims.Returns(mock);

        // Act
        Result<ClaimResponse> result = await _getClaimQueryHandler.Handle(ClaimQuery);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(1);
        result.Value.UCR.Should().Be("UCR");
        result.Value.AssuredName.Should().Be("AssuredName");
        result.Value.CompanyId.Should().Be(1);
        result.Value.ClaimTypeId.Should().Be(1);
        result.Value.CompanyName.Should().Be("Company 1");
        result.Value.ClaimTypeName.Should().Be("Claim Type 1");
        result.Value.ClaimDate.Should().Be(UtcNow.AddDays(-7));
        result.Value.NumberOfDaysOld.Should().Be(7);
    }
}
