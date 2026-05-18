using DuelRecords.Blazor.Components;
using DuelRecords.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(o => o.DetailedErrors = true)
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 32 * 1024 * 1024;
    });

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IDeckApiService, DeckApiService>();
builder.Services.AddScoped<ICardApiService, CardApiService>();
builder.Services.AddScoped<IImageApiService, ImageApiService>();
builder.Services.AddScoped<IDeckPrefsApiService, DeckPrefsApiService>();
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