using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WertinatWallify.Models;
using WertinatWallify.Services;
using WertinatWallify.Views;

namespace WertinatWallify.ViewModels
{
    public partial class GalleryViewModel : ObservableObject
    {
        private readonly IImageService _imageService;

        [ObservableProperty]
        private ObservableCollection<Wallpaper> wallpapers;

        public GalleryViewModel(IImageService imageService)
        {
            _imageService = imageService;
            Wallpapers = new ObservableCollection<Wallpaper>();
            LoadWallpapers();
        }

        private async void LoadWallpapers()
        {
            var wallpaperList = await _imageService.GetWallpapersAsync();
            foreach (var wallpaper in wallpaperList)
            {
                Wallpapers.Add(wallpaper);
            }
        }

        [RelayCommand]
        private async Task GoToDetails(Wallpaper selectedWallpaper)
        {
            if (selectedWallpaper == null)
                return;

            // Navigasyon parametresi olarak gönderme
            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedWallpaper", selectedWallpaper }
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), true, navigationParameter);
        }
    }
}