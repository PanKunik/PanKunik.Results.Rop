namespace PanKunik.Results.Rop.Tests;

public partial class ResultOfTExtensionsTests
{
    [Fact]
    public void FluentChain_WhenAllSuccess_ShouldExecuteAllSteps()
    {
        // Arrange
        var tapped = false;

        // Act
        var result = Result<int>
            .Success(1)
            .Map(v => v + 1)
            .Bind(v => Result<int>.Success(v * 2))
            .Tap(_ => tapped = true);

        // Assert
        Assert.True(tapped);
        Assert.True(result.IsSuccess);
        Assert.Equal(4, result.Value);
    }

    [Fact]
    public void FluentChain_WhenFailure_ShouldSkipAllSteps()
    {
        // Arrange
        var mapCalled = false;
        var bindCalled = false;
        var tapCalled = false;

        // Act
        var result = Result<int>
            .Failure(Error.Failure("CODE", "MESSAGE"))
            .Map(v =>
            {
                mapCalled = true;
                return v + 1;
            })
            .Bind(v =>
            {
                bindCalled = true;
                return Result<int>.Success(v);
            })
            .Tap(_ => tapCalled = true);

        // Assert
        Assert.False(mapCalled);
        Assert.False(bindCalled);
        Assert.False(tapCalled);
        Assert.True(result.IsFailure);
    }
}