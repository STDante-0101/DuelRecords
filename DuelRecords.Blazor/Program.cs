using DuelRecords.Blazor.Components;
using DuelRecords.Blazor.Services;
using DuelRecords.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IDeckApiService, DeckApiService>();
builder.Services.AddScoped<ICardApiService, CardApiService>();
builder.Services.AddScoped<YgoCatalogApiService>();
builder.Services.AddScoped<AppState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();