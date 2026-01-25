namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTExtensionsTests
{
    [Fact]
    public void Map_WhenSuccess_ShouldExecuteFunc()
    {
        // Act
        var result = Result<int>
            .Success(1)
            .Map(r => r.ToString());
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("1", result.Value);
    }

    [Fact]
    public void Map_WhenFailure_ShouldNotInvokeFunc()
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