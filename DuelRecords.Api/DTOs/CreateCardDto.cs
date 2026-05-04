using System.ComponentModel.DataAnnotations;
using DuelRecords.Api.Models;

namespace DuelRecords.Api.DTOs
{
    public class CreateCardDto
    {
        [Required(ErrorMessage = "O nome da Carta é obrigatorio bobo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo da carta é obrigatório.")]
        public TipoCard TipoCard { get; set; }

        [Required(ErrorMessage = "O atributo da carta é obrigatório.")]
        public string Atributo { get; set; } = string.Empty;
        public int? Nivel { get; set; }
        public int? Ataque { get; set; }
        public int? Defesa { get; set; }

        [Required(ErrorMessage = "A descrição da carta é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
    }
}
