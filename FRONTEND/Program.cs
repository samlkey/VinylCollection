using FRONTEND.Components;
using FRONTEND.Data;
using FRONTEND.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Use project root for DB path so dotnet watch run always uses the same file
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "VinylCollection.db");

// Database Configuration
builder.Services.AddDbContext<VinylDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Services
builder.Services.AddScoped<IVinylService, VinylService>();
builder.Services.AddHttpClient<IColorAnalysisService, ColorAnalysisService>();
builder.Services.AddScoped<ICurrentAlbumColorService, CurrentAlbumColorService>();

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VinylDbContext>();
    var colorService = scope.ServiceProvider.GetRequiredService<IColorAnalysisService>();
    // Delete the database file if it exists to ensure a fresh DB each run
    if (File.Exists(dbPath))
    {
        File.Delete(dbPath);
    }
    context.Database.EnsureCreated();
    await SeedData.SeedDatabaseAsync(context, colorService);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
