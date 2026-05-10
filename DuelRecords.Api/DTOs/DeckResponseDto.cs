namespace DuelRecords.Api.DTOs.Decks;

public class DeckResponseDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataAtualizacao { get; set; }

    public List<DeckCardResponseDto> Cards { get; set; } = new();
}

public class DeckCardResponseDto
{
    public int Id { get; set; }

    public int CardId { get; set; }

    public string NomeCard { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public string TipoDeck { get; set; } = string.Empty;

    public string? ImagemUrl { get; set; }

    public int Quantidade { get; set; }

    public string Secao { get; set; } = string.Empty;

    public int Ordem { get; set; }
}