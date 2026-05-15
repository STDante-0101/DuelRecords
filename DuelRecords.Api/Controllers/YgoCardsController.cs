using DuelRecords.Api.Services.YgoCatalogService;
using Microsoft.AspNetCore.Mvc;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/ygo/cards")]
public class YgoCardsController : ControllerBase
{
    private readonly YgoCatalogService _catalogService;

    public YgoCardsController(YgoCatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
    {
        var cards = await _catalogService.SearchByNameAsync(nome);

        return Ok(cards);
    }

    [HttpGet("{ygoId:int}")]
    public async Task<IActionResult> BuscarPorYgoId(int ygoId)
    {
        var card = await _catalogService.GetByYgoIdAsync(ygoId);

        if (card == null)
        {
            return NotFound();
        }

        return Ok(card);
    }
}