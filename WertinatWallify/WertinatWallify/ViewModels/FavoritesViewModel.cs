using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using WertinatWallify.Services;

namespace WertinatWallify.ViewModels
{
    public partial class FavoritesViewModel : ObservableObject
    {
        private readonly IImageService _imageService;
        private readonly IFavoritesService _favoritesService;

        // GalleryViewModel'deki gibi bir koleksiyon
        public ObservableCollection<WallpaperViewModel> FavoriteWallpapers { get; } = new();
        [ObservableProperty] // YENİ: Boş favori sayfası mesajını kontrol etmek için
        private bool hasNoFavorites;
        public FavoritesViewModel(IImageService imageService, IFavoritesService favoritesService)
        {
            _imageService = imageService;
            _favoritesService = favoritesService;
        }

        [RelayCommand]
        private async Task LoadFavorites()
        {
            FavoriteWallpapers.Clear();

            var allWallpapers = await _imageService.GetWallpapersAsync();
            var favoriteIds = await _favoritesService.GetFavoriteIds();

            var favs = allWallpapers.Where(w => favoriteIds.Contains(w.Id));

            foreach (var wallpaper in favs)
            {
                FavoriteWallpapers.Add(new WallpaperViewModel(wallpaper, true));
            }

            HasNoFavorites = !FavoriteWallpapers.Any();
        }

        // Bu sayfada da favoriden çıkarmak için
        [RelayCommand]
        private async Task ToggleFavorite(WallpaperViewModel wallpaperVM)
        {
            if (wallpaperVM == null) return;

            await _favoritesService.ToggleFavorite(wallpaperVM.Wallpaper.Id);
            // Animasyonlu bir şekilde listeden kaldırmak daha şık olabilir,
            // ama şimdilik direkt siliyoruz.
            FavoriteWallpapers.Remove(wallpaperVM);
            HasNoFavorites = !FavoriteWallpapers.Any();
        }
    }
}