using DuelRecords.Api.Models;
using Microsoft.EntityFrameworkCore;
using DuelRecords.Api.Models.Ygo;

namespace DuelRecords.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Deck> Decks { get; set; }

        public DbSet<DeckCard> DeckCards { get; set; }

        public DbSet<YgoCard> YgoCards { get; set; }

        public DbSet<YgoCardImage> YgoCardImages { get; set; }

        public DbSet<YgoCardSet> YgoCardSets { get; set; }

        public DbSet<YgoCardPrice> YgoCardPrices { get; set; }

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

            modelBuilder.Entity<YgoCard>()
                .HasIndex(c => c.YgoId)
                .IsUnique();

            modelBuilder.Entity<YgoCardImage>()
                .HasOne(i => i.YgoCard)
                .WithMany(c => c.Images)
                .HasForeignKey(i => i.YgoCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<YgoCardSet>()
                .HasOne(s => s.YgoCard)
                .WithMany(c => c.Sets)
                .HasForeignKey(s => s.YgoCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<YgoCardPrice>()
                .HasOne(p => p.YgoCard)
                .WithOne(c => c.Prices)
                .HasForeignKey<YgoCardPrice>(p => p.YgoCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}