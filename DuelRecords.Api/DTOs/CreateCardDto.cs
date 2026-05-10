using System.ComponentModel.DataAnnotations;

namespace DuelRecords.Api.DTOs
{
    public class CreateCardDto
    {
        [Required(ErrorMessage = "O nome da carta é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo da carta é obrigatório.")]
        public string Tipo { get; set; } = string.Empty;

        public string? Atributo { get; set; }

        public int? Nivel { get; set; }

        public int? Ataque { get; set; }

        public int? Defesa { get; set; }

        public string TipoDeck { get; set; } = "Normal";

        public string Raridade { get; set; } = "Comum";

        public int Quantidade { get; set; } = 1;

        public string Colecao { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public string? ImagemUrl { get; set; }
    }
}