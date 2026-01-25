namespace PanKunik.Results.Rop.Tests;

public partial class TaskResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task BindAsync_WhenSuccess_ShouldInvokeFunc()
    {
        // Arrange
        var taskResult = Task.FromResult(Result<int>.Success(1));

        // Act
        var result = await taskResult.BindAsync(v => Task.FromResult(Result<int>.Success(v + 2)));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public async Task BindAsync_WhenSuccessFuncReturnsFailure_ShouldReturnFailure()
    {
        // Arrange
        var taskResult = Task.FromResult(Result<int>.Success(1));
        var error = Error.Failure("NEW", "ERROR");

        // Act
        var result = await taskResult.BindAsync(_ => Task.FromResult(Result<int>.Failure(error)));

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
    
    [Fact]
    public async Task BindAsync_WhenFailure_ShouldNotInvokeFunc()
    {
        // Arrange
        var called = false;
        var error = Error.Failure("CODE", "MESSAGE");
        var taskResult = Task.FromResult(Result<int>.Failure(error));

        // Act
        var result = await taskResult.BindAsync(v =>
        {
            called = true;
            return Task.FromResult(Result<int>.Success(v));
        });

        // Assert
        Assert.False(called);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
}