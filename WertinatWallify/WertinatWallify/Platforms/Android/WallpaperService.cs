using Android.App;
using WertinatWallify.Services;
using Android.Graphics;
using WertinatWallify.Services.WertinatWallify.Services;

namespace WertinatWallify.Platforms.Android
{
    public class WallpaperService : IWallpaperService
    {
        public async Task<bool> SetWallpaperAsync(byte[] imageBytes, WallpaperTarget target)
        {
            try
            {
                var wallpaperManager = WallpaperManager.GetInstance(Platform.AppContext);
                using var bitmap = await BitmapFactory.DecodeByteArrayAsync(imageBytes, 0, imageBytes.Length);

                if (bitmap == null) return false;

                switch (target)
                {
                    case WallpaperTarget.HomeScreen:
                        wallpaperManager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.System);
                        break;
                    case WallpaperTarget.LockScreen:
                        wallpaperManager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.Lock);
                        break;
                    case WallpaperTarget.Both:
                        // Android API 24+ (Nougat) ve üzeri için
                        if (OperatingSystem.IsAndroidVersionAtLeast(24))
                        {
                            wallpaperManager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.System);
                            wallpaperManager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.Lock);
                        }
                        else // Eski versiyonlar için sadece ana ekran
                        {
                            wallpaperManager.SetBitmap(bitmap, null, true, WallpaperManagerFlags.System);
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Android wallpaper set failed: {ex.Message}");
                return false;
            }
        }
    }
}