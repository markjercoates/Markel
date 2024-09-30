using Markel.Application.Claims.AddClaim;
using FluentAssertions;
using Markel.Application.Abstractions.Data;
using Markel.Application.Abstractions.Repositories;
using Markel.Application.Abstractions.Results;
using NSubstitute;

namespace Markel.UnitTests.Claims;

public class AddClaimTests
{
    private static readonly DateTime UtcNow = DateTime.UtcNow;
    private static readonly AddClaimCommand Command = new AddClaimCommand("UCR", 1,
        1, UtcNow, UtcNow.AddDays(-1),
        "AssuredName", 1000, false);

    private readonly IClaimRepository _claimRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly AddClaimCommandHandler _addClaimCommandHandler;

    public AddClaimTests()
    {
        _claimRepositoryMock = Substitute.For<IClaimRepository>();
       _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _addClaimCommandHandler = new AddClaimCommandHandler(_unitOfWorkMock,_claimRepositoryMock);
    }

    [Fact]
    public async Task AddClaimHandler_Should_ReturnSuccess()
    {
        // Arrange
        _unitOfWorkMock.SaveChangesAsync().Returns(1);

        // Act
        Result<int> result = await _addClaimCommandHandler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}