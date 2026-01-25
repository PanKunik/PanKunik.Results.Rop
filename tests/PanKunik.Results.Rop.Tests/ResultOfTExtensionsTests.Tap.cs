namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTExtensionsTests
{
    [Fact]
    public void Tap_WhenSuccess_ShouldInvokeAction()
    {
        // Act
        var result = Result<int>
            .Success(1)
            .Map(r => (r + 2).ToString());
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("3", result.Value);
    }

    [Fact]
    public void Tap_WhenFailure_ShouldNotInvokeAction()
    {
        // Arrange
        var called = false;
        var error = Error.Failure("CODE", "MESSAGE");
        
        // Act
        var result = Result<int>
            .Failure(error)
            .Map(r =>
            {
                called = true;
                return r.ToString();
            });
        
        // Assert
        Assert.False(called);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
}