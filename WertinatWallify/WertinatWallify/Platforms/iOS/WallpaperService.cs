using Photos;
using UIKit;
using WertinatWallify.Services;
using WertinatWallify.Services.WertinatWallify.Services;

namespace WertinatWallify.Platforms.iOS
{
    public class WallpaperService : IWallpaperService
    {
        public async Task<bool> SetWallpaperAsync(byte[] imageBytes, WallpaperTarget target)
        {
            try
            {
                var image = new UIImage(Foundation.NSData.FromArray(imageBytes));

                // Fotoğraf galerisine kaydetme izni iste
                var status = await PHPhotoLibrary.RequestAuthorizationAsync();
                if (status != PHAuthorizationStatus.Authorized)
                {
                    // Kullanıcı izin vermedi
                    await App.Current.MainPage.DisplayAlert("İzin Gerekli", "Görseli kaydetmek için lütfen fotoğraf galerisi erişim izni verin.", "Tamam");
                    return false;
                }

                // Görseli galeriye kaydet
                bool success = false;
                PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() =>
                {
                    PHAssetChangeRequest.FromImage(image);
                }, (isSuccess, error) => {
                    success = isSuccess;
                });

                if (success)
                {
                    // Kullanıcıyı bilgilendir
                    await App.Current.MainPage.DisplayAlert("Kaydedildi!", "Duvar kağıdı fotoğraf galerinize kaydedildi. Ayarlar > Duvar Kağıdı bölümünden ayarlayabilirsiniz.", "Harika!");
                }

                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"iOS save to photos failed: {ex.Message}");
                return false;
            }
        }
    }
}