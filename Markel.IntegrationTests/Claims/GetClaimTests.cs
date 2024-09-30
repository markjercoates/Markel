using Markel.Application.Abstractions.Results;
using Markel.Application.Claims;
using Markel.Application.Claims.GetClaim;
using FluentAssertions;
using Markel.Application.IntegrationTests.Common;

namespace Markel.Application.IntegrationTests.Companies;

public class GetClaimTests : BaseIntegrationTest
{
    public GetClaimTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task GetClaim_ShouldReturnFailure_WhenClaimIsNotFound()
    {
        // Arrange
        var query = new GetClaimQuery(99);

        // Act
        Result<ClaimResponse> result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(ClaimErrors.NotFound);
    }

    [Fact]
    public async Task GetClaim_ShouldReturnSuccess_WithDaysOldClaim_WhenClaimIsFound()
    {
        // Arrange
        var query = new GetClaimQuery(1);

        // Act
        Result<ClaimResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(1);
        result.Value.NumberOfDaysOld.Should().Be(1);
        result.Value.CompanyName.Should().Be("Company 1");
        result.Value.ClaimTypeName.Should().Be("Claim Type 1");
    }

    
}
