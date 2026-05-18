using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public interface IDeckPrefsApiService
{
    Task<DeckCoverPrefs> GetPrefsAsync(int deckId);
    Task SavePrefsAsync(int deckId, DeckCoverPrefs prefs);
}
