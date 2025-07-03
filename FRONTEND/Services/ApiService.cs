using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FRONTEND.Data;
using Microsoft.AspNetCore.Mvc.Routing;

public class ApiService
{
    private readonly HttpClient _http;
    private readonly string url = ""; 

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Album>> GetAlbumsAsync()
    {
        // return await _http.GetFromJsonAsync<List<Album>>(url) ?? new List<Album>();

        return new List<Album>()
        {
            new Album()
            {
                Name = "MMM..FOOD",
                Artist = "MF DOOM",
                PrimaryColour = "#535b59",
                Rating = 2f,
                Release = 2004,
                Length = 48.54f,
                ImgSrc = "assets/img/album.jpg",
                ImgSrcBack = "assets/img/mmfood_back.jpg",
                TrackList = new List<Track>()
                {
                    new Track() { Name = "Beef Rapp", Length = 4.39f },
                    new Track() { Name = "Hoe Cakes", Length = 3.54f },
                    new Track() { Name = "Potholderz", Length = 3.20f },
                    new Track() { Name = "One Beer", Length = 4.18f },
                    new Track() { Name = "Deep Fried Frenz", Length = 4.59f },
                    new Track() { Name = "Poo-Putt Platter", Length = 1.13f },
                    new Track() { Name = "Fillet-O-Rapper", Length = 1.03f },
                    new Track() { Name = "Gumbo", Length = 0.49f },
                    new Track() { Name = "Fig Leaf Bi-Carbonate", Length = 3.19f },
                    new Track() { Name = "Kon Karne", Length = 2.51f },
                    new Track() { Name = "Guinnessez", Length = 4.41f },
                    new Track() { Name = "Kon Queso", Length = 4.0f },
                    new Track() { Name = "Rapp Snitch Knishes", Length = 2.52f },
                    new Track() { Name = "Vomitspit", Length = 2.48f },
                    new Track() { Name = "Kookies", Length = 4.01f }
                }
            },
            new Album()
            {
                Name = "TYRON",
                Artist = "SlowThai",
                PrimaryColour = "#63534d",
                Rating = 3.5f,
                Release = 2021,
                Length = 35.14f,
                ImgSrc = "assets/img/tyron.jpg",
                ImgSrcBack = "assets/img/tyron_back.webp",
                TrackList = new List<Track>()
                {
                    new Track() { Name = "Airbag", Length = 4.39f },
                    new Track() { Name = "Paranoid Android", Length = 6.23f },
                    new Track() { Name = "Subterranean Homesick Alien", Length = 4.27f },
                    new Track() { Name = "Exit Music (For a Film)", Length = 4.24f },
                    new Track() { Name = "Let Down", Length = 4.59f },
                    new Track() { Name = "Karma Police", Length = 4.21f },
                    new Track() { Name = "Filter Happier", Length = 1.57f },
                    new Track() { Name = "Electioneering", Length = 3.50f },
                    new Track() { Name = "Climbing Up the Walls", Length = 4.45f },
                    new Track() { Name = "No Surprises", Length = 3.48f },
                    new Track() { Name = "Lucky", Length = 4.19f },
                    new Track() { Name = "The Tourist", Length = 5.24f },
                    new Track() { Name = "Lucky", Length = 4.19f },
                    new Track() { Name = "The Tourist", Length = 5.24f }
                }
            },
            new Album()
            {
                Name = "PLASTIC BEACH",
                Artist = "Gorillaz",
                PrimaryColour = "#9e4f29",
                Rating = 5f,
                Release = 2010,
                Length = 56.53f,
                ImgSrc = "assets/img/plastic.jpg",
                ImgSrcBack = "assets/img/plastic_back.jpg",
                TrackList = new List<Track>()
                {
                    new Track() { Name = "Airbag", Length = 4.39f },
                    new Track() { Name = "Paranoid Android", Length = 6.23f },
                    new Track() { Name = "Subterranean Homesick Alien", Length = 4.27f },
                    new Track() { Name = "Exit Music (For a Film)", Length = 4.24f },
                    new Track() { Name = "Let Down", Length = 4.59f },
                    new Track() { Name = "Karma Police", Length = 4.21f },
                    new Track() { Name = "Filter Happier", Length = 1.57f },
                    new Track() { Name = "Electioneering", Length = 3.50f },
                    new Track() { Name = "Climbing Up the Walls", Length = 4.45f },
                    new Track() { Name = "No Surprises", Length = 3.48f },
                    new Track() { Name = "Lucky", Length = 4.19f },
                    new Track() { Name = "The Tourist", Length = 5.24f },
                    new Track() { Name = "Lucky", Length = 4.19f },
                    new Track() { Name = "The Tourist", Length = 5.24f }
                }
            }
        };
    }
}