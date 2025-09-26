using System.Text.Json;

namespace WertinatWallify.Services
{
    public class FavoritesService : IFavoritesService
    {
        private const string FavoritesKey = "favorite_wallpapers";

        public async Task<List<int>> GetFavoriteIds()
        {
            // Cihaz hafızasından favori ID'lerini okuyoruz
            string idsJson = Preferences.Get(FavoritesKey, "[]");
            return JsonSerializer.Deserialize<List<int>>(idsJson) ?? new List<int>();
        }

        public async Task ToggleFavorite(int wallpaperId)
        {
            var favoriteIds = await GetFavoriteIds();
            if (favoriteIds.Contains(wallpaperId))
            {
                favoriteIds.Remove(wallpaperId);
            }
            else
            {
                favoriteIds.Add(wallpaperId);
            }

            // Güncel listeyi tekrar cihaz hafızasına yazıyoruz
            string idsJson = JsonSerializer.Serialize(favoriteIds);
            Preferences.Set(FavoritesKey, idsJson);
        }

        public async Task<bool> IsFavorite(int wallpaperId)
        {
            var favoriteIds = await GetFavoriteIds();
            return favoriteIds.Contains(wallpaperId);
        }
    }
}