using LeCompteEstBon.Services;
using Xunit;

namespace LeCompteEstBon.Tests;

public class SolverServiceTests
{
    private readonly SolverService _solver = new();

    [Fact]
    public void Solve_FindsExactSolution_WhenPossible()
    {
        // Arrange
        var numbers = new List<int> { 25, 50, 75, 3, 6, 9, 2 };
        int target = 952;

        // Act
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 10);

        // Assert
        Assert.NotNull(solution);
        Assert.Equal(0, solution.Distance); // Should find exact match or very close
        Assert.True(solution.Steps.Count > 0); // Should have at least one step
    }

    [Fact]
    public void Solve_ReturnsClosestSolution_WhenExactNotPossible()
    {
        // Arrange
        var numbers = new List<int> { 1, 1, 1, 1, 1, 1, 1 };
        int target = 100;

        // Act
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 5);

        // Assert
        Assert.NotNull(solution);
        Assert.True(solution.Distance > 0); // Won't find exact with all 1s
        Assert.True(solution.Result > 0); // Should return some result
    }

    [Fact]
    public void Solve_HandlesSimpleCase()
    {
        // Arrange
        var numbers = new List<int> { 10, 5 };
        int target = 15;

        // Act
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 5);

        // Assert
        Assert.NotNull(solution);
        Assert.Equal(15, solution.Result);
        Assert.Equal(0, solution.Distance);
    }

    [Fact]
    public void Solve_HandlesMultiplication()
    {
        // Arrange
        var numbers = new List<int> { 5, 5, 2 };
        int target = 50;

        // Act
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 5);

        // Assert
        Assert.NotNull(solution);
        Assert.Equal(50, solution.Result);
        Assert.Equal(0, solution.Distance);
    }

    [Fact]
    public void Solve_HandlesDivision()
    {
        // Arrange
        var numbers = new List<int> { 100, 4, 1 };
        int target = 25;

        // Act
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 5);

        // Assert
        Assert.NotNull(solution);
        Assert.Equal(25, solution.Result);
        Assert.Equal(0, solution.Distance);
    }

    [Fact]
    public void Solve_WithSingleNumber_ReturnsThatNumber()
    {
        // Arrange
        var numbers = new List<int> { 42 };
        int target = 100;

        // Act
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 5);

        // Assert
        Assert.NotNull(solution);
        Assert.Equal(42, solution.Result);
        Assert.Equal(58, solution.Distance);
    }

    [Fact]
    public void Solve_RespectsTimeout()
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        int target = 999;

        // Act
        var startTime = DateTime.Now;
        var solution = _solver.Solve(numbers, target, timeoutSeconds: 1);
        var elapsed = DateTime.Now - startTime;

        // Assert
        Assert.NotNull(solution);
        Assert.True(elapsed.TotalSeconds < 2); // Should not exceed timeout significantly
    }
}
