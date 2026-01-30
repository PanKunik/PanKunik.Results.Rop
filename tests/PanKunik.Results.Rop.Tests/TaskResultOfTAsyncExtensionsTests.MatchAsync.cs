namespace PanKunik.Results.Rop.Tests;

public partial class TaskResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task Match_WhenSuccess_ShouldInvokeOnSuccessAction()
    {
        // Arrange
        var successCalled = false;
        var failureCalled = false;
        
        var taskResult = Task.FromResult(Result<int>.Success(1));

        // Act
        var result = await taskResult.MatchAsync(
            v =>
            {
                successCalled = true;
                return Task.FromResult(v + 2);
            },
            e =>
            {
                failureCalled = true;
                return Task.FromResult(0);
            });

        // Assert
        Assert.True(successCalled);
        Assert.False(failureCalled);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task MatchAsync_WhenFailure_ShouldInvokeOnFailureAction()
    {
        // Arrange
        var successCalled = false;
        var failureCalled = false;
        
        var taskResult = Task.FromResult(Result<int>.Failure(Error.Failure("CODE", "MESSAGE")));

        // Act
        var result = await taskResult.MatchAsync(
            v =>
            {
                successCalled = true;
                return Task.FromResult(v + 2);
            },
            e =>
            {
                failureCalled = true;
                return Task.FromResult(0);
            });

        // Assert
        Assert.False(successCalled);
        Assert.True(failureCalled);
        Assert.Equal(0, result);
    }
}