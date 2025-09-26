namespace WertinatWallify.Services
{
    public interface IFavoritesService
    {
        Task<List<int>> GetFavoriteIds();
        Task ToggleFavorite(int wallpaperId);
        Task<bool> IsFavorite(int wallpaperId);
    }
}