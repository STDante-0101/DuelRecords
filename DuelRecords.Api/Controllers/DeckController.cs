using DuelRecords.Api.DTOs.Decks;
using DuelRecords.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController : ControllerBase
{
    private readonly IDeckService _deckService;

    public DecksController(IDeckService deckService)
    {
        _deckService = deckService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DeckResponseDto>>> GetDecks()
    {
        var decks = await _deckService.GetAllAsync();
        return Ok(decks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DeckResponseDto>> GetDeckById(int id)
    {
        var deck = await _deckService.GetByIdAsync(id);

        if (deck == null)
        {
            return NotFound();
        }

        return Ok(deck);
    }

    [HttpPost]
    public async Task<ActionResult<DeckResponseDto>> CreateDeck(CreateDeckDto dto)
    {
        var deck = await _deckService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetDeckById),
            new { id = deck.Id },
            deck
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDeck(int id, UpdateDeckDto dto)
    {
        var updated = await _deckService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeck(int id)
    {
        var deleted = await _deckService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}