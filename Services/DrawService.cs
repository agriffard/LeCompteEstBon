namespace LeCompteEstBon.Services;

public class DrawService
{
    private readonly Random _random = new();
    
    // Small numbers (1-10), typically 6 available
    private readonly int[] _smallNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    
    // Large numbers, typically 1 available
    private readonly int[] _largeNumbers = { 25, 50, 75, 100 };

    public (List<int> numbers, int target) GenerateRandomDraw(int smallCount = 6, int largeCount = 1)
    {
        var numbers = new List<int>();
        
        // Add small numbers
        for (int i = 0; i < smallCount; i++)
        {
            numbers.Add(_smallNumbers[_random.Next(_smallNumbers.Length)]);
        }
        
        // Add large numbers
        for (int i = 0; i < largeCount; i++)
        {
            numbers.Add(_largeNumbers[_random.Next(_largeNumbers.Length)]);
        }
        
        // Generate target between 100 and 999
        int target = _random.Next(100, 1000);
        
        return (numbers, target);
    }
}
