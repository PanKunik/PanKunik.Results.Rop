namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTExtensionsTests
{
    [Fact]
    public void Bind_WhenSuccess_ShouldInvokeAction()
    {
        // Arrange
        var firstResult = Result<int>.Success(1);
        
        // Act
        var result = firstResult.Bind(v => Result<int>.Success(v + 2));
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void Bind_WhenSuccessFuncReturnsFailure_ShouldReturnNewFailure()
    {
        // Arrange
        var error = Error.Failure("NEW", "ERROR");
        
        // Act
        var result = Result<int>
            .Success(1)
            .Bind(_ => Result<int>.Failure(error));
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Bind_WhenFailure_ShouldNotInvokeFunc()
    {
        // Arrange
        var called = false;
        var error = Error.Failure("CODE", "MESSAGE");
        
        // Act
        var result =  Result<int>
            .Failure(error)
            .Bind(v =>
            {
                called = true;
                return Result<int>.Success(v);
            });
        
        // Assert
        Assert.False(called);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Bind_ShouldPropagateFirstFailure()
    {
        // Arrange
        var firstError = Error.Failure("CODE", "MESSAGE");
        
        // Act
        var result =  Result<int>
            .Failure(firstError)
            .Bind(v => Result<int>.Failure(Error.Failure("SECOND", "ERROR")));
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(firstError, result.Error);
    }
}