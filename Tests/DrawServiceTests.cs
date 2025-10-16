using LeCompteEstBon.Services;
using Xunit;

namespace LeCompteEstBon.Tests;

public class DrawServiceTests
{
    private readonly DrawService _drawService = new();

    [Fact]
    public void GenerateRandomDraw_ReturnsCorrectNumberCount()
    {
        // Act
        var (numbers, target) = _drawService.GenerateRandomDraw();

        // Assert
        Assert.Equal(7, numbers.Count);
    }

    [Fact]
    public void GenerateRandomDraw_TargetInValidRange()
    {
        // Act
        var (numbers, target) = _drawService.GenerateRandomDraw();

        // Assert
        Assert.True(target >= 100);
        Assert.True(target < 1000);
    }

    [Fact]
    public void GenerateRandomDraw_WithCustomCounts()
    {
        // Act
        var (numbers, target) = _drawService.GenerateRandomDraw(smallCount: 5, largeCount: 2);

        // Assert
        Assert.Equal(7, numbers.Count);
        Assert.True(target >= 100);
        Assert.True(target < 1000);
    }

    [Fact]
    public void GenerateRandomDraw_ContainsValidNumbers()
    {
        // Act
        var (numbers, target) = _drawService.GenerateRandomDraw();

        // Assert
        Assert.All(numbers, n => Assert.True(n > 0));
        Assert.All(numbers, n => Assert.True(n <= 100));
    }

    [Fact]
    public void GenerateRandomDraw_ProducesVariation()
    {
        // Act
        var draw1 = _drawService.GenerateRandomDraw();
        var draw2 = _drawService.GenerateRandomDraw();
        var draw3 = _drawService.GenerateRandomDraw();

        // Assert - at least one should be different (statistically almost certain)
        bool allSame = 
            draw1.target == draw2.target && draw2.target == draw3.target &&
            draw1.numbers.SequenceEqual(draw2.numbers) && 
            draw2.numbers.SequenceEqual(draw3.numbers);
        
        Assert.False(allSame);
    }
}
