using DuelRecords.Api.Models.Ygo;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Data.Contexts;

public class AlexandriaDbContext : DbContext
{
    public AlexandriaDbContext(DbContextOptions<AlexandriaDbContext> options)
        : base(options)
    {
    }

    public DbSet<YgoCard> YgoCards { get; set; }
    public DbSet<YgoCardImage> YgoCardImages { get; set; }
    public DbSet<YgoCardSet> YgoCardSets { get; set; }
    public DbSet<YgoCardPrice> YgoCardPrices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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