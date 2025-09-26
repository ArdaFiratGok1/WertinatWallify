using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq; // YENİ
using WertinatWallify.Models;
using WertinatWallify.Services;
using WertinatWallify.Views;

namespace WertinatWallify.ViewModels
{
    public partial class GalleryViewModel : ObservableObject
    {
        private readonly IImageService _imageService;
        private readonly IFavoritesService _favoritesService;
        private List<WallpaperViewModel> _allWallpapers = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        public ObservableCollection<WallpaperViewModel> Wallpapers { get; } = new();

        public GalleryViewModel(IImageService imageService, IFavoritesService favoritesService)
        {
            _imageService = imageService;
            _favoritesService = favoritesService;
        }

        [RelayCommand]
        private async Task LoadWallpapers()
        {
            // Veriyi sadece ilk seferde yüklüyoruz ki her seferinde sıfırlanmasın
            if (_allWallpapers.Any())
            {
                await RefreshFavoritesStatus(); // YENİ: Sadece favori durumlarını tazele
                return;
            }

            var wallpaperList = await _imageService.GetWallpapersAsync();
            var favoriteIds = await _favoritesService.GetFavoriteIds();

            foreach (var wallpaper in wallpaperList)
            {
                var isFav = favoriteIds.Contains(wallpaper.Id);
                _allWallpapers.Add(new WallpaperViewModel(wallpaper, isFav));
            }
            FilterWallpapers();
        }

        // YENİ METOT: Favori durumlarını hafızadaki ana kaynaktan kontrol edip günceller
        [RelayCommand]
        private async Task RefreshFavoritesStatus()
        {
            var favoriteIds = await _favoritesService.GetFavoriteIds();
            foreach (var wallpaperVM in _allWallpapers)
            {
                wallpaperVM.IsFavorite = favoriteIds.Contains(wallpaperVM.Wallpaper.Id);
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterWallpapers();
        }

        private void FilterWallpapers()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allWallpapers
                : _allWallpapers.Where(w => w.Wallpaper.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            Wallpapers.Clear();
            foreach (var wallpaper in filtered)
            {
                Wallpapers.Add(wallpaper);
            }
        }

        [RelayCommand]
        private async Task GoToDetails(WallpaperViewModel selectedWallpaperVM)
        {
            if (selectedWallpaperVM == null)
                return;

            var navigationParameter = new Dictionary<string, object>
    {
        { "SelectedWallpaper", selectedWallpaperVM.Wallpaper }
    };
            await Shell.Current.GoToAsync(nameof(DetailPage), true, navigationParameter);
        }

        [RelayCommand]
        private async Task ToggleFavorite(WallpaperViewModel wallpaperVM)
        {
            if (wallpaperVM == null) return;

            await _favoritesService.ToggleFavorite(wallpaperVM.Wallpaper.Id);
            wallpaperVM.IsFavorite = !wallpaperVM.IsFavorite;
        }
    }
}