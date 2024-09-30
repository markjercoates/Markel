using Markel.Application.Claims.UpdateClaim;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using Markel.Application.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Markel.UnitTests.Claims;

public class UpdateClaimTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly UpdateClaimCommand Command = new UpdateClaimCommand(1, "UCR", 1,
        1,  UtcNow, UtcNow.AddDays(-1),
        "AssuredName", 1000, false);

    private readonly IClaimRepository _claimRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly UpdateClaimCommandHandler _updateClaimCommandHandler;

    public UpdateClaimTests()
    {
        _claimRepositoryMock = Substitute.For<IClaimRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _updateClaimCommandHandler = new UpdateClaimCommandHandler(_unitOfWorkMock, 
            _claimRepositoryMock);
    }

    [Fact]
    public async Task UpdateClaimHandler_Should_ReturnFailure_When_ClaimId_NotFound()
    {
        // Arrange
        _claimRepositoryMock.GetByIdAsync(Command.Id).ReturnsNull();

        // Act
        var result = await _updateClaimCommandHandler.Handle(Command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateClaimHandler_Should_ReturnSuccess_When_Update_Succeeds()
    {
        // Arrange
        var claim = new Claim() { Id = 1, UCR = "UCR", AssuredName = "AssuredName" };

        _unitOfWorkMock.SaveChangesAsync().Returns(1);
        _claimRepositoryMock.GetByIdAsync(Command.Id).Returns(claim);

        // Act
        var result = await _updateClaimCommandHandler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}