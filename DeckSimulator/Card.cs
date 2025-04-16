public class Card
{
    public string Name { get; set; }
    public int Count { get; set; }
    public string CardType { get; set; } // "Monster", "Spell", "Trap"
    public bool? IsTuner { get; set; }
    public string Attribute { get; set; }
    public string Race { get; set; }
    public int? Level { get; set; }
    public List<string> Categories { get; set; }

    public override string ToString()
    {
        var categoryStr = Categories != null ? $" [{string.Join(",", Categories)}]" : "";
        return $"{Name} x{Count}{categoryStr}";
    }
}
