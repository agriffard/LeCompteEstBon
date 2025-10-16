namespace LeCompteEstBon.Models;

public class GameState
{
    public List<int> Numbers { get; set; } = new();
    public int Target { get; set; }
    public string? UserExpression { get; set; }
    public int? UserResult { get; set; }
    public Solution? BestSolution { get; set; }
}
