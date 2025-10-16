namespace LeCompteEstBon.Models;

public class Solution
{
    public string Expression { get; set; } = string.Empty;
    public int Result { get; set; }
    public int Distance { get; set; }
    public List<string> Steps { get; set; } = new();
}
