namespace DuelRecords.Api.Models;

public class DeckCard
{
    public int Id { get; set; }

    public int DeckId { get; set; }

    public Deck Deck { get; set; } = default!;

    public int CardId { get; set; }

    public Card Card { get; set; } = default!;

    public int Quantidade { get; set; } = 1;

    public DeckSection Secao { get; set; } = DeckSection.Main;

    public int Ordem { get; set; }
}