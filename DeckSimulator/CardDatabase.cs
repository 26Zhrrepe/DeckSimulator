using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class CardDatabase
{
    private static List<Card>? _cards;

    public static void Load(string path)
    {
        string json = File.ReadAllText(path);
        _cards = JsonSerializer.Deserialize<List<Card>>(json);
    }

    public static Card? FindByName(string name)
    {
        return _cards?.FirstOrDefault(c => c.Name == name);
    }
}
