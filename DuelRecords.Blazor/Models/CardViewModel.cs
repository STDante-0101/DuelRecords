namespace DuelRecords.Blazor.Models
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int TipoCard { get; set; }
        public string Atributo { get; set; } = string.Empty;
        public int? Nivel { get; set; }
        public int? Ataque { get; set; }
        public int? Defesa { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime  DataCadastro { get; set; }
    }
}
