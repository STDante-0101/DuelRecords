using DuelRecords.Api.DTOs;
using DuelRecords.Api.Models;
using DuelRecords.Api.Services;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Card>>> GetCards()
    {
        var cards = await _cardService.GetAllAsync();

        return Ok(cards);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetCardById(int id)
    {
        var card = await _cardService.GetByIdAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        return Ok(card);
    }

    [HttpPost]
    public async Task<ActionResult<Card>> CreateCard(CreateCardDto dto)
    {
        var card = await _cardService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCard(int id, UpdateCardDto dto)
    {
        var updated = await _cardService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCard(int id)
    {
        var deleted = await _cardService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("exportar-excel")]
    public async Task<IActionResult> ExportarExcel()
    {
        var cards = await _cardService.GetAllAsync();

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Cards");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 2).Value = "Nome";
        worksheet.Cell(1, 3).Value = "Tipo";
        worksheet.Cell(1, 4).Value = "Atributo";
        worksheet.Cell(1, 5).Value = "Nivel";
        worksheet.Cell(1, 6).Value = "Ataque";
        worksheet.Cell(1, 7).Value = "Defesa";
        worksheet.Cell(1, 8).Value = "Descrição";
        worksheet.Cell(1, 9).Value = "Data Cadastro";

        var linha = 2;

        foreach (var card in cards)
        {
            worksheet.Cell(linha, 1).Value = card.Id;
            worksheet.Cell(linha, 2).Value = card.Nome;
            worksheet.Cell(linha, 3).Value = card.TipoCard.ToString();
            worksheet.Cell(linha, 4).Value = card.Atributo;
            worksheet.Cell(linha, 5).Value = card.Nivel;
            worksheet.Cell(linha, 6).Value = card.Ataque;
            worksheet.Cell(linha, 7).Value = card.Defesa;
            worksheet.Cell(linha, 8).Value = card.Descricao;
            worksheet.Cell(linha, 9).Value = card.DataCadastro;

            linha++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        var content = stream.ToArray();

        return File(
            content,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "cards.xlsx"
        );
    }
}
