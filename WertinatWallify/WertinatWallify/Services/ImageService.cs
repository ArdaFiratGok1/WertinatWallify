// GÜNCELLENDİ
using WertinatWallify.Models;

namespace WertinatWallify.Services
{
    public class ImageService : IImageService // Önceki caching kodumuz burada geçerli
    {
        private readonly List<Wallpaper> _wallpapers = new List<Wallpaper>
        {
            
            new Wallpaper { Id = 1, Name = "Dağ Gölü", ThumbnailUrl = "https://picsum.photos/id/1/200/300", FullImageUrl = "https://picsum.photos/id/1/1080/1920" },
            new Wallpaper { Id = 2, Name = "Ahşap Zemin", ThumbnailUrl = "https://picsum.photos/id/10/200/300", FullImageUrl = "https://picsum.photos/id/10/1080/1920" },
            new Wallpaper { Id = 3, Name = "Bilgisayar Başı", ThumbnailUrl = "https://picsum.photos/id/20/200/300", FullImageUrl = "https://picsum.photos/id/20/1080/1920" },
            new Wallpaper { Id = 4, Name = "Kahve Molası", ThumbnailUrl = "https://picsum.photos/id/30/200/300", FullImageUrl = "https://picsum.photos/id/30/1080/1920" },
            new Wallpaper { Id = 5, Name = "Kitaplar", ThumbnailUrl = "https://picsum.photos/id/40/200/300", FullImageUrl = "https://picsum.photos/id/40/1080/1920" },
            new Wallpaper { Id = 6, Name = "Şehir Manzarası", ThumbnailUrl = "https://picsum.photos/id/50/200/300", FullImageUrl = "https://picsum.photos/id/50/1080/1920" },
            new Wallpaper { Id = 7, Name = "Sahil Yolu", ThumbnailUrl = "https://picsum.photos/id/60/200/300", FullImageUrl = "https://picsum.photos/id/60/1080/1920" },
            new Wallpaper { Id = 8, Name = "Sisli Orman", ThumbnailUrl = "https://picsum.photos/id/75/200/300", FullImageUrl = "https://picsum.photos/id/75/1080/1920" },
            new Wallpaper { Id = 9, Name = "Gece Işıkları", ThumbnailUrl = "https://picsum.photos/id/82/200/300", FullImageUrl = "https://picsum.photos/id/82/1080/1920" },
            new Wallpaper { Id = 10, Name = "Minimalist Masa", ThumbnailUrl = "https://picsum.photos/id/99/200/300", FullImageUrl = "https://picsum.photos/id/99/1080/1920" },
            new Wallpaper { Id = 11, Name = "Dokulu Duvar", ThumbnailUrl = "https://picsum.photos/id/102/200/300", FullImageUrl = "https://picsum.photos/id/102/1080/1920" },
            new Wallpaper { Id = 12, Name = "Sonbahar Yaprakları", ThumbnailUrl = "https://picsum.photos/id/111/200/300", FullImageUrl = "https://picsum.photos/id/111/1080/1920" },
            new Wallpaper { Id = 13, Name = "Kanyon Manzarası", ThumbnailUrl = "https://picsum.photos/id/124/200/300", FullImageUrl = "https://picsum.photos/id/124/1080/1920" },
            new Wallpaper { Id = 14, Name = "Mimari Detay", ThumbnailUrl = "https://picsum.photos/id/137/200/300", FullImageUrl = "https://picsum.photos/id/137/1080/1920" },
            new Wallpaper { Id = 15, Name = "Buzul Zirvesi", ThumbnailUrl = "https://picsum.photos/id/145/200/300", FullImageUrl = "https://picsum.photos/id/145/1080/1920" },
            new Wallpaper { Id = 16, Name = "Sakin Deniz", ThumbnailUrl = "https://picsum.photos/id/152/200/300", FullImageUrl = "https://picsum.photos/id/152/1080/1920" },
            new Wallpaper { Id = 17, Name = "Çalışma Alanı", ThumbnailUrl = "https://picsum.photos/id/160/200/300", FullImageUrl = "https://picsum.photos/id/160/1080/1920" },
            new Wallpaper { Id = 18, Name = "Metro İstasyonu", ThumbnailUrl = "https://picsum.photos/id/175/200/300", FullImageUrl = "https://picsum.photos/id/175/1080/1920" },
            new Wallpaper { Id = 19, Name = "Kırsal Ev", ThumbnailUrl = "https://picsum.photos/id/180/200/300", FullImageUrl = "https://picsum.photos/id/180/1080/1920" },
            new Wallpaper { Id = 20, Name = "Soyut Desen", ThumbnailUrl = "https://picsum.photos/id/200/200/300", FullImageUrl = "https://picsum.photos/id/200/1080/1920" }
        };

        // ... GetWallpapersAsync ve GetImageBytesAsync metotları aynı kalıyor ...
        private readonly HttpClient _httpClient = new();

        public Task<List<Wallpaper>> GetWallpapersAsync()
        {
            return Task.FromResult(_wallpapers);
        }

        public async Task<byte[]?> GetImageBytesAsync(string imageUrl)
        {
            var fileName = imageUrl.Replace("https://", "").Replace("/", "_").Replace(".", "_") + ".jpg";
            var localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            if (File.Exists(localPath))
            {
                return await File.ReadAllBytesAsync(localPath);
            }
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }
            try
            {
                var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
                await File.WriteAllBytesAsync(localPath, imageBytes);
                return imageBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image download failed: {ex.Message}");
                return null;
            }
        }
    }
}