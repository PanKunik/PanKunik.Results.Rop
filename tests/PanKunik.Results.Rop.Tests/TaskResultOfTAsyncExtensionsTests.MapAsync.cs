namespace PanKunik.Results.Rop.Tests;

public partial class TaskResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task MapAsync_WhenSuccess_ShouldExecuteFunc()
    {
        // Arrange
        var taskResult = Task.FromResult(Result<int>.Success(2));

        // Act
        var result = await taskResult.MapAsync(v => Task.FromResult(v.ToString()));

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
        var taskResult = Task.FromResult(Result<int>.Failure(error));

        // Act
        var result = await taskResult.MapAsync(v =>
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