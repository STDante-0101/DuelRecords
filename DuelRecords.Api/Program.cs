using DuelRecords.Api.Data;
using DuelRecords.Api.Services;
using Microsoft.EntityFrameworkCore;
using DuelRecords.Api.Services.YgoServices;
using DuelRecords.Api.Data.Contexts;

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
    mundoZeroDbContext.Database.Migrate();

    var alexandriaDbContext = scope.ServiceProvider.GetRequiredService<AlexandriaDbContext>();
    alexandriaDbContext.Database.Migrate();
}

app.Run();