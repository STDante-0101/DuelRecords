using DuelRecords.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
    public DbSet<DeckPrefs> DeckPrefs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================================================
        // FIX UTC GLOBAL PARA POSTGRESQL + NPGSQL
        // =========================================================

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var dateTimeProperties = entityType
                .GetProperties()
                .Where(p => p.ClrType == typeof(DateTime));

            foreach (var property in dateTimeProperties)
            {
                property.SetValueConverter(
                    new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                    )
                );
            }

            var nullableDateTimeProperties = entityType
                .GetProperties()
                .Where(p => p.ClrType == typeof(DateTime?));

            foreach (var property in nullableDateTimeProperties)
            {
                property.SetValueConverter(
                    new ValueConverter<DateTime?, DateTime?>(
                        v => v.HasValue
                            ? v.Value.ToUniversalTime()
                            : v,
                        v => v.HasValue
                            ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                            : v
                    )
                );
            }
        }

        // =========================================================
        // RELACIONAMENTOS
        // =========================================================

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

        modelBuilder.Entity<DeckPrefs>()
            .HasOne(p => p.Deck)
            .WithOne()
            .HasForeignKey<DeckPrefs>(p => p.DeckId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DeckPrefs>()
            .HasIndex(p => p.DeckId)
            .IsUnique();
    }
}