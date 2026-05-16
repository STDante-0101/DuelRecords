namespace DuelRecords.Api.Models;

public class Deck
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime DataCriacao { get; set; } 

    public DateTime DataAtualizacao { get; set; } 

    public List<DeckCard> Cards { get; set; } = new();
}