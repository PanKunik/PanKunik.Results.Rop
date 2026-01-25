namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTExtensionsTests
{
    [Fact]
    public void BindIf_WhenConditionFalse_ShouldNotInvokeAction()
    {
        // Arrange
        var called = false;
        
        // Act
        var result = Result<int>
            .Success(1)
            .BindIf(false, v =>
            {
                called = true;
                return Result<int>.Success(v);
            });
        
        // Assert
        Assert.False(called);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
    }
    
    [Fact]
    public void BindIf_WhenConditionFalse_ShouldPreserveFailure()
    {
        // Arrange
        var firstError = Error.Failure("CODE", "MESSAGE");
        
        // Act
        var result = Result<int>
            .Failure(firstError)
            .BindIf(false, v => Result<int>.Failure(Error.Failure("SECOND", "ERROR")));
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(firstError, result.Error);
    }

    [Fact]
    public void BindIf_WhenConditionTrueAndSuccess_ShouldInvokeFunc()
    {
        // Act
        var result = Result<int>
            .Success(1)
            .BindIf(true, v => Result<int>.Success(v + 2));
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void BindIf_WhenConditionTrueAndFuncReturnsFailure_ShouldReturnNewFailure()
    {
        // Arrange
        var error = Error.Failure("CODE", "MESSAGE");
        
        // Act
        var result = Result<int>
            .Success(1)
            .BindIf(true, _ => Result<int>.Failure(error));
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
}