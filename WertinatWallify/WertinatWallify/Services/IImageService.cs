using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WertinatWallify.Models;

namespace WertinatWallify.Services
{
    public interface IImageService
    {
        Task<List<Wallpaper>> GetWallpapersAsync();
        Task<byte[]> GetImageBytesAsync(string imageUrl);
    }
}
