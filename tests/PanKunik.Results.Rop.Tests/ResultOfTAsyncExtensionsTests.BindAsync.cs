namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task BindAsync_WhenSuccess_ShouldInvokeFunc()
    {
        // Act
        var result = await Result<int>
            .Success(1)
            .BindAsync(v => Task.FromResult(Result<int>.Success(v + 2)));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public async Task BindAsync_WhenSuccessFuncReturnsFailure_ShouldReturnFailure()
    {
        // Arrange
        var error = Error.Failure("NEW", "ERROR");

        // Act
        var result = await Result<int>
            .Success(1)
            .BindAsync(_ => Task.FromResult(Result<int>.Failure(error)));

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

        // Act
        var result = await Result<int>
            .Failure(error)
            .BindAsync(v =>
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