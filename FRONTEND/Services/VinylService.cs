using FRONTEND.Data;
using Microsoft.EntityFrameworkCore;

namespace FRONTEND.Services
{
    public interface IVinylService
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<Album?> GetAlbumByIdAsync(int id);
        Task<Album> AddAlbumAsync(Album album);
        Task<Album> UpdateAlbumAsync(Album album);
        Task DeleteAlbumAsync(int id);
        Task<List<Track>> GetTracksByAlbumIdAsync(int albumId);
        Task<Track> AddTrackAsync(Track track);
        Task DeleteTrackAsync(int id);
        Task<List<Album>> SearchAlbumsAsync(string query);
        Task<List<Album>> GetRandomAlbumsAsync(int count = 5);
    }

    public class VinylService : IVinylService
    {
        private readonly VinylDbContext _context;

        public VinylService(VinylDbContext context)
        {
            _context = context;
        }

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await _context.Albums
                .Include(a => a.TrackList)
                .ToListAsync();
        }

        public async Task<Album?> GetAlbumByIdAsync(int id)
        {
            return await _context.Albums
                .Include(a => a.TrackList)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Album> AddAlbumAsync(Album album)
        {
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task<Album> UpdateAlbumAsync(Album album)
        {
            _context.Albums.Update(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task DeleteAlbumAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Track>> GetTracksByAlbumIdAsync(int albumId)
        {
            return await _context.Tracks
                .Where(t => t.AlbumId == albumId)
                .ToListAsync();
        }

        public async Task<Track> AddTrackAsync(Track track)
        {
            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();
            return track;
        }

        public async Task DeleteTrackAsync(int id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Album>> SearchAlbumsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Album>();
            var lowered = query.ToLower();
            return await _context.Albums
                .Include(a => a.TrackList)
                .Where(a => a.Name.ToLower().Contains(lowered) || a.Artist.ToLower().Contains(lowered))
                .ToListAsync();
        }

        public async Task<List<Album>> GetRandomAlbumsAsync(int count = 5)
        {
            return await _context.Albums
                .Include(a => a.TrackList)
                .OrderBy(a => EF.Functions.Random())
                .Take(count)
                .ToListAsync();
        }
    }
}