using DuelRecords.Api.DTOs.Decks;

namespace DuelRecords.Api.Services;

public interface IDeckService
{
    Task<List<DeckResponseDto>> GetAllAsync();
    Task<DeckResponseDto?> GetByIdAsync(int id);
    Task<DeckResponseDto> CreateAsync(CreateDeckDto dto);
    Task<bool> UpdateAsync(int id, UpdateDeckDto dto);
    Task<bool> DeleteAsync(int id);
}