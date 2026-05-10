namespace DuelRecords.Blazor.Models;

public class DeckApiModel
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataAtualizacao { get; set; }

    public List<DeckCardApiModel> Cards { get; set; } = new();
}

public class DeckCardApiModel
{
    public int Id { get; set; }

    public int CardId { get; set; }

    public string NomeCard { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public string TipoDeck { get; set; } = string.Empty;

    public string? ImagemUrl { get; set; }

    public int Quantidade { get; set; }

    public string Secao { get; set; } = "Main";

    public int Ordem { get; set; }
}

public class CreateDeckApiModel
{
    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public List<CreateDeckCardApiModel> Cards { get; set; } = new();
}

public class CreateDeckCardApiModel
{
    public int CardId { get; set; }

    public int Quantidade { get; set; }

    public int Secao { get; set; }

    public int Ordem { get; set; }
}