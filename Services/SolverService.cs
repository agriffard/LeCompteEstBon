using LeCompteEstBon.Models;

namespace LeCompteEstBon.Services;

public class SolverService
{
    private class State
    {
        public List<long> Numbers { get; set; } = new();
        public List<string> Steps { get; set; } = new();
    }
    
    public Solution? Solve(List<int> numbers, int target, int timeoutSeconds = 5)
    {
        var startTime = DateTime.Now;
        var timeout = TimeSpan.FromSeconds(timeoutSeconds);
        Solution? bestSolution = null;
        int bestDistance = int.MaxValue;
        
        var initialState = new State
        {
            Numbers = numbers.Select(n => (long)n).ToList(),
            Steps = new()
        };
        
        Search(initialState, target, ref bestSolution, ref bestDistance, startTime, timeout);
        
        return bestSolution;
    }
    
    private void Search(State state, int target, ref Solution? bestSolution, ref int bestDistance, DateTime startTime, TimeSpan timeout)
    {
        // Check timeout
        if (DateTime.Now - startTime > timeout)
            return;
        
        // Check all numbers for potential solutions
        foreach (var num in state.Numbers)
        {
            int distance = Math.Abs((int)num - target);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestSolution = new Solution
                {
                    Result = (int)num,
                    Distance = distance,
                    Expression = num.ToString(),
                    Steps = new List<string>(state.Steps)
                };
                
                // Found exact solution
                if (distance == 0)
                    return;
            }
        }
        
        // If only one number left, no more operations possible
        if (state.Numbers.Count == 1)
            return;
        
        // Try all pairs of numbers with all operations
        for (int i = 0; i < state.Numbers.Count; i++)
        {
            for (int j = i + 1; j < state.Numbers.Count; j++)
            {
                long a = state.Numbers[i];
                long b = state.Numbers[j];
                
                // Try all operations
                var operations = new List<(long result, string expr)>();
                
                // Addition
                operations.Add((a + b, $"{a} + {b} = {a + b}"));
                
                // Subtraction (both ways)
                if (a > b)
                    operations.Add((a - b, $"{a} - {b} = {a - b}"));
                if (b > a)
                    operations.Add((b - a, $"{b} - {a} = {b - a}"));
                
                // Multiplication
                operations.Add((a * b, $"{a} × {b} = {a * b}"));
                
                // Division (both ways, only if divisible)
                if (b != 0 && a % b == 0)
                    operations.Add((a / b, $"{a} ÷ {b} = {a / b}"));
                if (a != 0 && b % a == 0)
                    operations.Add((b / a, $"{b} ÷ {a} = {b / a}"));
                
                foreach (var (result, expr) in operations)
                {
                    // Create new state with result
                    var newNumbers = new List<long>(state.Numbers);
                    newNumbers.RemoveAt(j); // Remove in reverse order to maintain indices
                    newNumbers.RemoveAt(i);
                    newNumbers.Add(result);
                    
                    var newSteps = new List<string>(state.Steps) { expr };
                    
                    var newState = new State
                    {
                        Numbers = newNumbers,
                        Steps = newSteps
                    };
                    
                    Search(newState, target, ref bestSolution, ref bestDistance, startTime, timeout);
                    
                    // Early exit if exact solution found
                    if (bestDistance == 0)
                        return;
                }
            }
        }
    }
}
