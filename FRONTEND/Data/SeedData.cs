using FRONTEND.Data;
using System.Text.Json;

namespace FRONTEND.Data
{
    public static class SeedData
    {
        public static void SeedDatabase(VinylDbContext context)
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
                        
                        // Ensure all required fields have values
                        foreach (var album in albums)
                        {
                            album.Name ??= "Unknown Album";
                            album.Artist ??= "Unknown Artist";
                            album.PrimaryColour ??= "#000000";
                            album.ImgSrc ??= "assets/img/album.jpg";
                            album.ImgSrcBack ??= "assets/img/album.jpg";
                            album.TrackList ??= new List<Track>();
                            
                            // Ensure track names are not null
                            foreach (var track in album.TrackList)
                            {
                                track.Name ??= "Unknown Track";
                            }
                        }

                        context.Albums.AddRange(albums);
                        context.SaveChanges();
                        Console.WriteLine("Successfully seeded database from JSON");
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