namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task TapAsync_WithValue_WhenSuccess_ShouldInvokeAction()
    {
        // Arrange
        var called = false;

        // Act
        var result = await Result<int>
            .Success(1)
            .TapAsync(v =>
            {
                called = true;
                Assert.Equal(1, v);
                return Task.CompletedTask;
            });

        // Assert
        Assert.True(called);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public async Task TapAsync_WithValue_WhenFailure_ShouldNotInvokeAction()
    {
        // Arrange
        var called = false;

        // Act
        var result = await Result<int>
            .Failure(Error.Failure("CODE", "MESSAGE"))
            .TapAsync(v =>
            {
                called = true;
                return Task.CompletedTask;
            });

        // Assert
        Assert.False(called);
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public async Task TapAsync_WithoutValue_WhenSuccess_ShouldInvokeAction()
    {
        // Arrange
        var called = false;

        // Act
        var result = await Result<int>
            .Success(1)
            .TapAsync(() =>
            {
                called = true;
                return Task.CompletedTask;
            });

        // Assert
        Assert.True(called);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task TapAsync_WithoutValue_WhenFailure_ShouldNotInvokeAction()
    {
        // Arrange
        var called = false;

        // Act
        var result = await Result<int>
            .Failure(Error.Failure("CODE", "MESSAGE"))
            .TapAsync(() =>
            {
                called = true;
                return Task.CompletedTask;
            });

        // Assert
        Assert.False(called);
        Assert.True(result.IsFailure);
    }
}