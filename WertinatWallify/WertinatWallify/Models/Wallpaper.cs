using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WertinatWallify.Models
{
    public class Wallpaper
    {
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; } // Grid'de gösterilecek küçük resim
        public string FullImageUrl { get; set; } // Detay sayfasındaki büyük resim
        public string Name { get; set; } = string.Empty;
    }
}
