using CommunityToolkit.Mvvm.ComponentModel;
using WertinatWallify.Models;

namespace WertinatWallify.ViewModels
{
    // Bu sınıf, bir Wallpaper'ı ve onun favori durumunu birlikte tutar
    public partial class WallpaperViewModel : ObservableObject
    {
        public Wallpaper Wallpaper { get; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FavoriteIcon))]
        private bool isFavorite;

        public string FavoriteIcon => IsFavorite ? "heart_fill.png" : "heart_empty.png";

        public WallpaperViewModel(Wallpaper wallpaper, bool isFavorite)
        {
            Wallpaper = wallpaper;
            IsFavorite = isFavorite;
        }
    }
}