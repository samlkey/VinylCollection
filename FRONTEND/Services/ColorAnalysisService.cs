using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FRONTEND.Services
{
    public interface IColorAnalysisService
    {
        Task<string> GetDominantColorAsync(string imageUrl);
    }

    public class ColorAnalysisService : IColorAnalysisService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ColorAnalysisService> _logger;

        public ColorAnalysisService(HttpClient httpClient, ILogger<ColorAnalysisService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetDominantColorAsync(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return "#000000";

                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                
                using var image = Image.Load<Rgba32>(imageBytes);
                
                // Resize for performance (analyze every 10th pixel)
                image.Mutate(x => x.Resize(100, 100));
                
                var colorCounts = new Dictionary<Rgba32, int>();
                
                // Sample pixels and count colors
                for (int y = 0; y < image.Height; y += 10)
                {
                    var row = image.Frames.RootFrame.PixelBuffer.DangerousGetRowSpan(y);
                    for (int x = 0; x < image.Width; x += 10)
                    {
                        var pixel = row[x];
                        
                        // Group similar colors (reduce precision)
                        var roundedColor = new Rgba32(
                            (byte)(pixel.R / 16 * 16),
                            (byte)(pixel.G / 16 * 16),
                            (byte)(pixel.B / 16 * 16)
                        );
                        
                        colorCounts[roundedColor] = colorCounts.GetValueOrDefault(roundedColor, 0) + 1;
                    }
                }
                
                var validColors = colorCounts.Where(c => 
                {
                    var total = c.Key.R + c.Key.G + c.Key.B;
                    return total > 100 && total < 700; // Filter out very light/dark colors
                });
                
                if (!validColors.Any())
                {
                    validColors = colorCounts;
                }
                
                var dominantColor = validColors.OrderByDescending(x => x.Value).First().Key;
                
                var hexColor = $"#{dominantColor.R:X2}{dominantColor.G:X2}{dominantColor.B:X2}";
                _logger.LogInformation($"Calculated color {hexColor} for image {imageUrl}");
                
                return hexColor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calculating color for image {imageUrl}");
                return "#000000"; // Fallback to black
            }
        }
    }
} 