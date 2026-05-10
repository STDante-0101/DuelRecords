using ClosedXML.Excel;
using DuelRecords.Api.DTOs;
using DuelRecords.Api.Models;
using DuelRecords.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IWebHostEnvironment _environment;

    public CardsController(
        ICardService cardService,
        IWebHostEnvironment environment)
    {
        _cardService = cardService;
        _environment = environment;
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

        return CreatedAtAction(
            nameof(GetCardById),
            new { id = card.Id },
            card
        );
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

    [HttpPost("upload-imagem")]
    public async Task<IActionResult> UploadImagem(IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest("Arquivo inválido.");
        }

        var extensao = Path.GetExtension(arquivo.FileName).ToLowerInvariant();

        var extensoesPermitidas = new[] { ".png", ".jpg", ".jpeg" };

        if (!extensoesPermitidas.Contains(extensao))
        {
            return BadRequest("Formato de imagem inválido. Use PNG, JPG ou JPEG.");
        }

        var webRootPath = _environment.WebRootPath;

        if (string.IsNullOrWhiteSpace(webRootPath))
        {
            webRootPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
        }

        var pastaUploads = Path.Combine(webRootPath, "uploads", "cards");

        if (!Directory.Exists(pastaUploads))
        {
            Directory.CreateDirectory(pastaUploads);
        }

        var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

        var caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        var urlImagem = $"/uploads/cards/{nomeArquivo}";

        return Ok(new
        {
            imagemUrl = urlImagem
        });
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
        worksheet.Cell(1, 5).Value = "Nível";
        worksheet.Cell(1, 6).Value = "Ataque";
        worksheet.Cell(1, 7).Value = "Defesa";
        worksheet.Cell(1, 8).Value = "Tipo Deck";
        worksheet.Cell(1, 9).Value = "Raridade";
        worksheet.Cell(1, 10).Value = "Quantidade";
        worksheet.Cell(1, 11).Value = "Coleção";
        worksheet.Cell(1, 12).Value = "Descrição";
        worksheet.Cell(1, 13).Value = "Imagem";
        worksheet.Cell(1, 14).Value = "Data Cadastro";

        var linha = 2;

        foreach (var card in cards)
        {
            worksheet.Cell(linha, 1).Value = card.Id;
            worksheet.Cell(linha, 2).Value = card.Nome;
            worksheet.Cell(linha, 3).Value = card.Tipo;
            worksheet.Cell(linha, 4).Value = card.Atributo;
            worksheet.Cell(linha, 5).Value = card.Nivel;
            worksheet.Cell(linha, 6).Value = card.Ataque;
            worksheet.Cell(linha, 7).Value = card.Defesa;
            worksheet.Cell(linha, 8).Value = card.TipoDeck;
            worksheet.Cell(linha, 9).Value = card.Raridade;
            worksheet.Cell(linha, 10).Value = card.Quantidade;
            worksheet.Cell(linha, 11).Value = card.Colecao;
            worksheet.Cell(linha, 12).Value = card.Descricao;
            worksheet.Cell(linha, 13).Value = card.ImagemUrl;
            worksheet.Cell(linha, 14).Value = card.DataCadastro;

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