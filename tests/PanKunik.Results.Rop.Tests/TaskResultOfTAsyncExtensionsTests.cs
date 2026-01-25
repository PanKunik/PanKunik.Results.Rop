namespace PanKunik.Results.Rop.Tests;

public partial class TaskResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task FluentAsyncChain_WhenAllSuccess_ShouldExecuteAllSteps()
    {
        // Arrange
        var tapped = false;
        var taskResult = Task.FromResult(Result<int>.Success(1));

        // Act
        var result = await taskResult
            .MapAsync(v => Task.FromResult(v + 1))
            .BindAsync(v => Task.FromResult(Result<int>.Success(v * 2)))
            .TapAsync(_ =>
            {
                tapped = true;
                return Task.CompletedTask;
            });

        // Assert
        Assert.True(tapped);
        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Value);
    }

    [Fact]
    public async Task FluentAsyncChain_WhenFailure_ShouldSkipAllSteps()
    {
        // Arrange
        var mapCalled = false;
        var bindCalled = false;
        var tapCalled = false;
        var taskResult = Task.FromResult(Result<int>.Failure(Error.Failure("CODE", "MESSAGE")));

        // Act
        var result = await taskResult
            .MapAsync(v =>
            {
                mapCalled = true;
                return Task.FromResult(v + 1);
            })
            .BindAsync(v =>
            {
                bindCalled = true;
                return Task.FromResult(Result<int>.Success(v));
            })
            .TapAsync(_ =>
            {
                tapCalled = true;
                return Task.CompletedTask;
            });

        // Assert
        Assert.False(mapCalled);
        Assert.False(bindCalled);
        Assert.False(tapCalled);
        Assert.True(result.IsFailure);
    }
}