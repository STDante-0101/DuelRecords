using DuelRecords.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Deck> Decks { get; set; }

        public DbSet<DeckCard> DeckCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Deck>()
                .HasMany(deck => deck.Cards)
                .WithOne(deckCard => deckCard.Deck)
                .HasForeignKey(deckCard => deckCard.DeckId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeckCard>()
                .HasOne(deckCard => deckCard.Card)
                .WithMany()
                .HasForeignKey(deckCard => deckCard.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeckCard>()
                .HasIndex(deckCard => new
                {
                    deckCard.DeckId,
                    deckCard.CardId,
                    deckCard.Secao
                })
                .IsUnique();
        }
    }
}