using System.ComponentModel.DataAnnotations;

namespace DuelRecords.Api.DTOs.Decks;

public class UpdateDeckDto
{
    [Required(ErrorMessage = "O nome do deck é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public List<DeckCardDto> Cards { get; set; } = new();
}