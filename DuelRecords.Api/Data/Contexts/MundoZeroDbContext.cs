using DuelRecords.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Data.Contexts;

public class MundoZeroDbContext : DbContext
{
    public MundoZeroDbContext(DbContextOptions<MundoZeroDbContext> options)
        : base(options)
    {
    }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<DeckCard> DeckCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeckCard>()
            .HasOne(dc => dc.Deck)
            .WithMany(d => d.Cards)
            .HasForeignKey(dc => dc.DeckId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DeckCard>()
            .HasOne(dc => dc.Card)
            .WithMany()
            .HasForeignKey(dc => dc.CardId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DeckCard>()
            .HasIndex(dc => new { dc.DeckId, dc.CardId, dc.Secao })
            .IsUnique();
    }
}