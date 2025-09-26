using CommunityToolkit.Maui;
using WertinatWallify.Services;
using WertinatWallify.ViewModels;
using WertinatWallify.Views;
using Microsoft.Extensions.Logging;
using WertinatWallify.Services.WertinatWallify.Services;

namespace WertinatWallify;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // Bu satır artık hata vermeyecek
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Genel servisleri kaydediyoruz
        builder.Services.AddSingleton<IImageService, ImageService>();
        builder.Services.AddSingleton<IFavoritesService, FavoritesService>();

        // --- Platforma özgü servisleri DOĞRU şekilde kaydediyoruz ---
#if ANDROID
        builder.Services.AddSingleton<IWallpaperService, Platforms.Android.WallpaperService>();
        builder.Services.AddSingleton<IPhotoSaverService, Platforms.Android.PhotoSaverService>();
#elif IOS
        builder.Services.AddSingleton<IWallpaperService, Platforms.iOS.WallpaperService>();
        builder.Services.AddSingleton<IPhotoSaverService, Platforms.iOS.PhotoSaverService>();
#endif
        // -----------------------------------------------------------

        // ViewModel ve View'leri kaydediyoruz
        builder.Services.AddSingleton<GalleryViewModel>();
        builder.Services.AddSingleton<GalleryPage>();

        builder.Services.AddTransient<DetailViewModel>();
        builder.Services.AddTransient<DetailPage>();

        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddTransient<FavoritesPage>();

        return builder.Build();
    }
}