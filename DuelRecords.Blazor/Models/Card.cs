namespace DuelRecords.Blazor.Models;

public class Card
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string? Attribute { get; set; }
    public int? Level { get; set; }
    public int? Atk { get; set; }
    public int? Def { get; set; }
    public string Kind { get; set; } = "Normal";
    public string Rarity { get; set; } = "Comum";
    public int Qty { get; set; } = 1;
    public string Set { get; set; } = "";
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}

public class DeckCardEntry
{
    public string Id { get; set; } = "";
    public string Section { get; set; } = "main";
}

public class Deck
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "Novo Deck";
    public string UpdatedAgo { get; set; } = "agora";
    public List<DeckCardEntry> Cards { get; set; } = new();
}

public class CardFilters
{
    public string? Q { get; set; }
    public string? Type { get; set; }
    public string? Attribute { get; set; }
    public string? Level { get; set; }
    public string? Kind { get; set; }
}

public class FrameColor
{
    public string Top { get; set; } = "";
    public string Bot { get; set; } = "";
    public string Border { get; set; } = "";
}

public static class CardCatalog
{
    public static readonly string[] Types = { "Monstro", "Magia", "Armadilha" };
    public static readonly string[] Attributes = { "Trevas", "Luz", "Fogo", "Água", "Terra", "Vento", "Divino" };
    public static readonly string[] Rarities = { "Comum", "Rara", "Super Rara", "Ultra Rara", "Secreta" };
    public static readonly string[] Kinds = { "Normal", "Efeito", "Fusão", "Ritual", "Sincro", "XYZ", "Link", "Pêndulo" };

    public static readonly Dictionary<string, string[]> ArtPalettes = new()
    {
        ["Trevas"] = new[] { "#3a1670", "#6d28d9", "#0a0312" },
        ["Luz"] = new[] { "#fde68a", "#f59e0b", "#7c2d12" },
        ["Fogo"] = new[] { "#fb923c", "#dc2626", "#450a0a" },
        ["Água"] = new[] { "#22d3ee", "#0e7490", "#082f49" },
        ["Terra"] = new[] { "#a16207", "#65a30d", "#1c1917" },
        ["Vento"] = new[] { "#86efac", "#15803d", "#052e16" },
        ["Divino"] = new[] { "#fef3c7", "#d97706", "#431407" },
    };

    public static FrameColor GetFrameColor(Card c)
    {
        if (c.Type == "Magia")
        {
            return new() { Top = "#1d8a5b", Bot = "#0f5c3c", Border = "#2bbf85" };
        }

        if (c.Type == "Armadilha")
        {
            return new() { Top = "#a83276", Bot = "#691f4c", Border = "#d65aa3" };
        }

        return c.Kind switch
        {
            "Fusão" => new() { Top = "#6d28d9", Bot = "#3b1080", Border = "#a78bfa" },
            "Ritual" => new() { Top = "#1e63b8", Bot = "#0e3d7a", Border = "#5fa8ff" },
            "Sincro" => new() { Top = "#dadada", Bot = "#9a9a9a", Border = "#ffffff" },
            "XYZ" => new() { Top = "#1a1a1a", Bot = "#0a0a0a", Border = "#3a3a3a" },
            "Link" => new() { Top = "#1a4ec9", Bot = "#0a2e7a", Border = "#5582f5" },
            "Efeito" => new() { Top = "#a8540e", Bot = "#6e3608", Border = "#d6822f" },
            _ => new() { Top = "#a8852b", Bot = "#6e5418", Border = "#e8c451" },
        };
    }

    public static bool IsExtraKind(Card c)
    {
        return c.Kind is "Fusão" or "Sincro" or "XYZ" or "Link";
    }

    public static int RarityRank(string rarity)
    {
        return rarity switch
        {
            "Comum" => 0,
            "Rara" => 1,
            "Super Rara" => 2,
            "Ultra Rara" => 3,
            "Secreta" => 4,
            _ => 0,
        };
    }
}