namespace DuelRecords.Api.Models;

public class Card
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoCard TipoCard { get; set; }
    public string Atributo { get; set; }
    public int? Nivel { get; set; }
    public int? Ataque { get; set; }
    public int? Defesa { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; } = DateTime.Now;

}

