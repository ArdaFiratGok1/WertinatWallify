using Microsoft.Extensions.Logging;
using WertinatWallify.Services;
using WertinatWallify.Services.WertinatWallify.Services;
using WertinatWallify.ViewModels;
using WertinatWallify.Views;

namespace WertinatWallify;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Servisleri kaydediyoruz
        builder.Services.AddSingleton<IImageService, ImageService>();

        // Platforma özgü servisleri kaydediyoruz
#if ANDROID
        builder.Services.AddSingleton<IWallpaperService, Platforms.Android.WallpaperService>();
#elif IOS
        builder.Services.AddSingleton<IWallpaperService, Platforms.iOS.WallpaperService>();
#endif

        // ViewModel ve View'leri kaydediyoruz
        builder.Services.AddSingleton<GalleryViewModel>();
        builder.Services.AddSingleton<GalleryPage>();

        builder.Services.AddTransient<DetailViewModel>(); // Detay sayfası her açıldığında yeni bir instance oluşsun
        builder.Services.AddTransient<DetailPage>();

        return builder.Build();
    }
}