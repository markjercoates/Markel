using Markel.Application.Abstractions.Results;
using Markel.Application.Companies;
using Markel.Application.Companies.GetCompany;
using FluentAssertions;
using Markel.Application.IntegrationTests.Common;

namespace Markel.Application.IntegrationTests.Companies;

public class GetCompanyTests : BaseIntegrationTest
{
    public GetCompanyTests(IntegrationTestWebAppFactory factory)
        :base(factory)
    {
        
    }    

    [Fact]
    public async Task GetCompany_ShouldReturnFailure_WhenCompanyIsNotFound()
    {
        // Arrange
        var query = new GetCompanyQuery(99);

        // Act
        Result<CompanyResponse> result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(CompanyErrors.NotFound);                
    }

    [Fact]
    public async Task GetCompany_ShouldReturnSuccess_WhenCompanyIsFound()
    {
        // Arrange
        var query = new GetCompanyQuery(1);

        // Act
        Result<CompanyResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();        
    }

    [Fact]
    public async Task GetCompany_ShouldReturn_PolicyNotActive_When_InsuranceEndDate_Past()
    {
        // Arrange
        var query = new GetCompanyQuery(2);

        // Act
        Result<CompanyResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.HasActivePolicy.Should().BeFalse();
        result.Value.InsuranceEndDate.Should().BeBefore(DateTime.Now);
    }

    [Fact]
    public async Task GetCompany_ShouldReturn_CompanyWithClaims()
    {
        // Arrange
        var query = new GetCompanyClaimsQuery(1);

        // Act
        Result<CompanyClaimsResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(1);
        result.Value.Claims.Count().Should().Be(2);
        result.Value.Claims.First().Id.Should().Be(1);
    }
}
