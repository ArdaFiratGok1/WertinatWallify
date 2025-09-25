using System.Net.Http;
using WertinatWallify.Models;

namespace WertinatWallify.Services
{
    public class ImageService : IImageService
    {
        // Gerçek bir API yerine örnek veri kullanıyoruz.
        // Görselleri internetten çekeceğiz. Unsplash gibi sitelerden link alabilirsiniz.
        private readonly List<Wallpaper> _wallpapers = new List<Wallpaper>
        {
            new Wallpaper { Id = 1, ThumbnailUrl = "https://picsum.photos/id/1/200/300", FullImageUrl = "https://picsum.photos/id/1/1080/1920" },
            new Wallpaper { Id = 2, ThumbnailUrl = "https://picsum.photos/id/10/200/300", FullImageUrl = "https://picsum.photos/id/10/1080/1920" },
            new Wallpaper { Id = 3, ThumbnailUrl = "https://picsum.photos/id/20/200/300", FullImageUrl = "https://picsum.photos/id/20/1080/1920" },
            new Wallpaper { Id = 4, ThumbnailUrl = "https://picsum.photos/id/30/200/300", FullImageUrl = "https://picsum.photos/id/30/1080/1920" },
            new Wallpaper { Id = 5, ThumbnailUrl = "https://picsum.photos/id/40/200/300", FullImageUrl = "https://picsum.photos/id/40/1080/1920" },
            new Wallpaper { Id = 6, ThumbnailUrl = "https://picsum.photos/id/50/200/300", FullImageUrl = "https://picsum.photos/id/50/1080/1920" }
        };

        private readonly HttpClient _httpClient = new HttpClient();

        public Task<List<Wallpaper>> GetWallpapersAsync()
        {
            return Task.FromResult(_wallpapers);
        }

        public async Task<byte[]> GetImageBytesAsync(string imageUrl)
        {
            try
            {
                // Görseli internetten indirip byte dizisine çeviriyoruz.
                var response = await _httpClient.GetAsync(imageUrl);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi burada yapılabilir.
                Console.WriteLine($"Image download failed: {ex.Message}");
            }
            return null;
        }
    }
}