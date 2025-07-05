using System.Text.Json.Serialization;

namespace FRONTEND.Data
{
    public class Track
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Length { get; set; }
        public int AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}