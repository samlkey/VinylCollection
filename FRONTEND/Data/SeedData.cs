using FRONTEND.Data;
using System.Text.Json;
using FRONTEND.Services;

namespace FRONTEND.Data
{
    public static class SeedData
    {
        public static async Task SeedDatabaseAsync(VinylDbContext context, IColorAnalysisService colorService)
        {
            if (context.Albums.Any())
                return; // Database already seeded

            try
            {
                // Load albums from JSON file
                var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "albums.json");
                Console.WriteLine($"Looking for JSON file at: {jsonPath}");
                
                if (File.Exists(jsonPath))
                {
                    Console.WriteLine("JSON file found, attempting to load...");
                    var jsonContent = File.ReadAllText(jsonPath);
                    Console.WriteLine($"JSON content length: {jsonContent.Length}");
                    
                    var albums = JsonSerializer.Deserialize<List<Album>>(jsonContent);
                    
                    if (albums != null && albums.Count > 0)
                    {
                        Console.WriteLine($"Successfully deserialized {albums.Count} albums");
                        
                        // Calculate colors for each album
                        foreach (var album in albums)
                        {
                            if (string.IsNullOrEmpty(album.Name))
                                album.Name = "Unknown Album";
                            if (string.IsNullOrEmpty(album.Artist))
                                album.Artist = "Unknown Artist";
                            
                            // Calculate primary color from album cover
                            await album.CalculatePrimaryColourAsync(colorService);
                            
                            // Ensure track names are not null
                            if (album.TrackList != null)
                            {
                                foreach (var track in album.TrackList)
                                {
                                    if (string.IsNullOrEmpty(track.Name))
                                        track.Name = "Unknown Track";
                                }
                            }
                        }

                        context.Albums.AddRange(albums);
                        await context.SaveChangesAsync();
                        Console.WriteLine("Successfully seeded database from JSON with calculated colors");
                    }
                    else
                    {
                        Console.WriteLine("JSON deserialization returned null or empty list");
                    }
                }
                else
                {
                    Console.WriteLine("JSON file not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON: {ex.Message}");
            }
        }
    }
} 