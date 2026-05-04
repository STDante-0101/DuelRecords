using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public interface ICardApiService
{
    Task<List<CardViewModel>> GetCardsAsync();
    Task<CardViewModel?> GetCardByIdAsync(int id);
    Task<bool> CreateCardAsync(CreateCardViewModel card);
    Task<bool> UpdateCardAsync(int id, CreateCardViewModel card);
    Task<bool> DeleteCardAsync(int id);
    string GetExportExcelUrl();
}
