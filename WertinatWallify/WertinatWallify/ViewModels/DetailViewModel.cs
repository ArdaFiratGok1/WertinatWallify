using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WertinatWallify.Models;
using WertinatWallify.Services;
using WertinatWallify.Services.WertinatWallify.Services;

namespace WertinatWallify.ViewModels
{
    // Navigasyon ile gelen veriyi almak için bu attribute'ü kullanıyoruz.
    [QueryProperty(nameof(SelectedWallpaper), "SelectedWallpaper")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly IImageService _imageService;
        private readonly IWallpaperService _wallpaperService;

        [ObservableProperty]
        private Wallpaper selectedWallpaper;

        public DetailViewModel(IImageService imageService, IWallpaperService wallpaperService)
        {
            _imageService = imageService;
            _wallpaperService = wallpaperService;
        }

        // Genel bir duvar kağıdı ayarlama metodu
        private async Task SetWallpaper(WallpaperTarget target)
        {
            if (SelectedWallpaper == null) return;

            // Büyük görseli indir
            var imageBytes = await _imageService.GetImageBytesAsync(SelectedWallpaper.FullImageUrl);
            if (imageBytes != null)
            {
                bool result = await _wallpaperService.SetWallpaperAsync(imageBytes, target);
                if (result && DeviceInfo.Platform == DevicePlatform.Android)
                {
                    await App.Current.MainPage.DisplayAlert("Başarılı", "Duvar kağıdı ayarlandı!", "Tamam");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Hata", "Görsel indirilemedi.", "Tamam");
            }
        }

        [RelayCommand]
        private async Task SetAsHomeScreen()
        {
            await SetWallpaper(WallpaperTarget.HomeScreen);
        }

        [RelayCommand]
        private async Task SetAsLockScreen()
        {
            await SetWallpaper(WallpaperTarget.LockScreen);
        }

        [RelayCommand]
        private async Task SetAsBoth()
        {
            await SetWallpaper(WallpaperTarget.Both);
        }

        [RelayCommand]
        private async Task Share()
        {
            if (SelectedWallpaper == null) return;
            await Microsoft.Maui.ApplicationModel.DataTransfer.Share.Default.RequestAsync(new ShareTextRequest

            {
                Uri = SelectedWallpaper.FullImageUrl,
                Title = "Bu harika duvar kağıdına bak!"
            });
        }
    }
}