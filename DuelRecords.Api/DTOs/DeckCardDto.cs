using DuelRecords.Api.Models;

namespace DuelRecords.Api.DTOs.Decks;

public class DeckCardDto
{
    public int CardId { get; set; }

    public int Quantidade { get; set; } = 1;

    public DeckSection Secao { get; set; } = DeckSection.Main;

    public int Ordem { get; set; }
}