using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public class AppState
{
    public event Action? OnChange;

    public List<Card> Cards { get; private set; } = new();

    public bool IsLoadingCards { get; private set; }
    public string? CardsError { get; private set; }

    public List<Deck> Decks { get; private set; } = new();

    public string Accent { get; private set; } = "purple";
    public int Cols { get; private set; } = 6;

    public static readonly Dictionary<string, (string c1, string c2, string glow)> AccentPresets = new()
    {
        ["purple"] = ("#8b5cf6", "#a78bfa", "rgba(139, 92, 246, 0.55)"),
        ["cyan"] = ("#22d3ee", "#67e8f9", "rgba(34, 211, 238, 0.55)"),
        ["gold"] = ("#d4a64a", "#fbbf24", "rgba(212, 166, 74, 0.55)"),
        ["crimson"] = ("#ef4444", "#f87171", "rgba(239, 68, 68, 0.55)")
    };

    public async Task LoadCardsFromApiAsync(ICardApiService cardApiService)
    {
        try
        {
            IsLoadingCards = true;
            CardsError = null;

            Notify();

            Cards = await cardApiService.GetCardsAsync();
        }
        catch (Exception ex)
        {
            CardsError = $"Erro ao carregar cards da API: {ex.Message}";
            Cards = new();
        }
        finally
        {
            IsLoadingCards = false;
            Notify();
        }
    }

    public async Task LoadDecksFromApiAsync(IDeckApiService deckApiService)
    {
        try
        {
            var apiDecks = await deckApiService.GetDecksAsync();

            Decks = apiDecks.Select(MapDeckFromApi).ToList();

            Notify();
        }
        catch
        {
            Decks = new();
            Notify();
        }
    }

    public async Task SaveDeckAsync(
        Deck deck,
        IDeckApiService deckApiService)
    {
        var dto = new CreateDeckApiModel
        {
            Nome = deck.Name,
            Cards = BuildDeckCards(deck)
        };

        if (int.TryParse(deck.Id, out var deckId))
        {
            await deckApiService.UpdateDeckAsync(deckId, dto);
        }
        else
        {
            await deckApiService.CreateDeckAsync(dto);
        }

        await LoadDecksFromApiAsync(deckApiService);
    }

    public async Task DeleteDeckAsync(
        Deck deck,
        IDeckApiService deckApiService)
    {
        if (int.TryParse(deck.Id, out var deckId))
        {
            await deckApiService.DeleteDeckAsync(deckId);
        }

        await LoadDecksFromApiAsync(deckApiService);
    }

    public void SetAccent(string accent)
    {
        Accent = accent;
        Notify();
    }

    public void SetCols(int cols)
    {
        Cols = cols;
        Notify();
    }

    public void AddCard(Card card)
    {
        Cards.Insert(0, card);
        Notify();
    }

    public Card? GetCard(string id)
    {
        return Cards.FirstOrDefault(c => c.Id == id);
    }

    public void Notify()
    {
        OnChange?.Invoke();
    }

    private Deck MapDeckFromApi(DeckApiModel apiDeck)
    {
        return new Deck
        {
            Id = apiDeck.Id.ToString(),
            Name = apiDeck.Nome,
            UpdatedAgo = "agora",
            Cards = apiDeck.Cards.Select(c => new DeckCardEntry
            {
                Id = c.CardId.ToString(),
                Section = c.Secao.ToLower()
            }).ToList()
        };
    }

    private List<CreateDeckCardApiModel> BuildDeckCards(Deck deck)
    {
        return deck.Cards
            .GroupBy(c => new
            {
                c.Id,
                c.Section
            })
            .Select((group, index) => new CreateDeckCardApiModel
            {
                CardId = int.Parse(group.Key.Id),
                Quantidade = group.Count(),
                Ordem = index + 1,
                Secao = group.Key.Section switch
                {
                    "main" => 1,
                    "extra" => 2,
                    "side" => 3,
                    _ => 1
                }
            })
            .ToList();
    }
}