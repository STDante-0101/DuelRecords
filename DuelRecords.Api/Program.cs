using DuelRecords.Api.Data;
using DuelRecords.Api.Services;
using Microsoft.EntityFrameworkCore;
using DuelRecords.Api.Services.YgoServices;
using DuelRecords.Api.Data.Contexts;
using DuelRecords.Api.Services.YgoCatalogService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MundoZeroDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MundoZeroConnection")));

builder.Services.AddDbContext<AlexandriaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AlexandriaConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IDeckService, DeckService>();
builder.Services.AddScoped<YgoImportService>();
builder.Services.AddScoped<YgoCatalogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var mundoZeroDbContext = scope.ServiceProvider.GetRequiredService<MundoZeroDbContext>();

    // Remove entrada inconsistente do histórico: migration marcada como aplicada mas tabela ausente
    try
    {
        mundoZeroDbContext.Database.ExecuteSqlRaw("""
            DELETE FROM "__EFMigrationsHistory"
            WHERE "MigrationId" = '20260517000001_AdicionarDeckPrefs'
              AND NOT EXISTS (
                SELECT 1 FROM information_schema.tables
                WHERE table_schema = 'public' AND table_name = 'DeckPrefs'
              )
            """);
    }
    catch { } // tabela de histórico pode ainda não existir no primeiro boot

    mundoZeroDbContext.Database.Migrate();

    var alexandriaDbContext = scope.ServiceProvider.GetRequiredService<AlexandriaDbContext>();
    alexandriaDbContext.Database.Migrate();
}

app.Run();