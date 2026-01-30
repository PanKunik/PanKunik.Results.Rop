namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTAsyncExtensionsTests
{
    [Fact]
    public async Task MatchAsync_WhenSuccess_ShouldInvokeOnSuccessAction()
    {
        // Arrange
        var successCalled = false;
        var failureCalled = false;

        // Act
        var result = await Result<int>
            .Success(1)
            .MatchAsync(
                v =>
                {
                    successCalled = true;
                    return Task.FromResult(v + 2);
                },
                e =>
                {
                    failureCalled = true;
                    return Task.FromResult(0);
                }
            );

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

        // Act
        var result = await Result<int>
            .Failure(Error.Validation("CODE", "MESSAGE"))
            .MatchAsync(
                v =>
                {
                    successCalled = true;
                    return Task.FromResult(v + 2);
                },
                e =>
                {
                    failureCalled = true;
                    return Task.FromResult(0);
                }
            );

        // Assert
        Assert.True(failureCalled);
        Assert.False(successCalled);
        Assert.Equal(0, result);
    }
}