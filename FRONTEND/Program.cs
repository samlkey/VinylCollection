using FRONTEND.Components;
using FRONTEND.Data;
using FRONTEND.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database Configuration
builder.Services.AddDbContext<VinylDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
        "Data Source=VinylCollection.db"));

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
