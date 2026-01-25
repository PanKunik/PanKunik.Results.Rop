namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task MapAsync_WhenSuccess_ShouldExecuteFunc()
    {
        // Act
        var result = await Result<int>
            .Success(2)
            .MapAsync(v => Task.FromResult(v.ToString()));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("2", result.Value);
    }

    [Fact]
    public async Task MapAsync_WhenFailure_ShouldNotInvokeFunc()
    {
        // Arrange
        var called = false;
        var error = Error.Failure("CODE", "MESSAGE");

        // Act
        var result = await Result<int>
            .Failure(error)
            .MapAsync(v =>
            {
                called = true;
                return Task.FromResult(v.ToString());
            });

        // Assert
        Assert.False(called);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
}