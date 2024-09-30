using Markel.Application.Abstractions.Results;
using FluentAssertions;
using Markel.Api.IntegrationTests.Common;
using Markel.Application.Claims.UpdateClaim;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Markel.API.Controllers.Claims;
using Markel.Application.Claims;

namespace Markel.Api.IntegrationTests.Claims;
public class UpdateClaimTests : BaseIntegrationTest
{
    public UpdateClaimTests(IntegrationTestWebApiFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task UpdateClaim_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Arrange
        var updateClaimRequest = new UpdateClaimRequest("", 1,
            1, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-7),
            "AssuredName", 1000, false);            

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/api/claims/1", updateClaimRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonSerializerOptions);
        problem!.Title.Should().Be("Validation Error");
    }

    [Fact]
    public async Task UpdateCompany_ShouldReturnOK_WhenValidRequest()
    {
        // Arrange
        var updateClaimRequest = new UpdateClaimRequest("UCR", 1,
            1, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-7),
            "AssuredNameChanged", 1000, false);

        // Act
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync("/api/claims/1", updateClaimRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response = await HttpClient.GetAsync("/api/claims/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var claim = await response.Content.ReadFromJsonAsync<ClaimResponse>();
        claim!.AssuredName.Should().Be("AssuredNameChanged");    
    }
}

