using Markel.Application.Abstractions.Results;
using Markel.Application.Companies;
using Markel.Application.Companies.AddCompany;
using FluentAssertions;
using Markel.Api.IntegrationTests.Common;
using Markel.Application.Companies.GetCompany;
using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Markel.Api.IntegrationTests.Companies;
public class AddCompanyTests : BaseIntegrationTest
{
    public AddCompanyTests(IntegrationTestWebApiFactory factory)
        : base(factory)
    {

    }

    [Fact]
    public async Task AddCompany_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Arrange
        var addCompanyCommand = new AddCompanyCommand("", "Address1",
            "Address2", "Address3", "PostCode",
            "Country", true, DateTime.Now.AddDays(7));

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/companies", addCompanyCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonSerializerOptions);
        problem!.Title.Should().Be("Validation Error");
    }

    [Fact]
    public async Task AddCompany_ShouldReturnOK_WhenValidRequest()
    {
        // Arrange
        var addCompanyCommand = new AddCompanyCommand("Name", "Address1",
            "Address2", "Address3", "PostCode",
            "Country", true, DateTime.Now.AddDays(7));

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/api/companies", addCompanyCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
