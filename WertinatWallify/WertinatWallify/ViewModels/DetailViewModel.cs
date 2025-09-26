using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WertinatWallify.Models;
using WertinatWallify.Services;
using WertinatWallify.Services.WertinatWallify.Services;

namespace WertinatWallify.ViewModels // Namespace'in doğru olduğundan emin olun
{
    [QueryProperty(nameof(SelectedWallpaper), "SelectedWallpaper")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly IImageService _imageService;
        private readonly IWallpaperService _wallpaperService;
        private readonly IPhotoSaverService _photoSaverService;

        [ObservableProperty]
        private Wallpaper? selectedWallpaper;

        public DetailViewModel(IImageService imageService, IWallpaperService wallpaperService, IPhotoSaverService photoSaverService)
        {
            _imageService = imageService;
            _wallpaperService = wallpaperService;
            _photoSaverService = photoSaverService;
        }

        private async Task SetWallpaper(WallpaperTarget target)
        {
            if (SelectedWallpaper == null) return;

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
        private async Task SetAsHomeScreen() => await SetWallpaper(WallpaperTarget.HomeScreen);

        [RelayCommand]
        private async Task SetAsLockScreen() => await SetWallpaper(WallpaperTarget.LockScreen);

        [RelayCommand]
        private async Task SetAsBoth() => await SetWallpaper(WallpaperTarget.Both);

        [RelayCommand]
        private async Task DownloadImage()
        {
            if (SelectedWallpaper == null) return;

            var imageBytes = await _imageService.GetImageBytesAsync(SelectedWallpaper.FullImageUrl);
            if (imageBytes != null)
            {
                string fileName = $"{SelectedWallpaper.Name.Replace(" ", "_")}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                bool result = await _photoSaverService.SavePhotoAsync(imageBytes, fileName);

                if (result)
                {
                    await App.Current.MainPage.DisplayAlert("Başarılı", "Görsel galerinize kaydedildi!", "Tamam");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Hata", "Görsel kaydedilemedi.", "Tamam");
                }
            }
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