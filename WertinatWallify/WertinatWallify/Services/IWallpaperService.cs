using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WertinatWallify.Services
{
    namespace WertinatWallify.Services
    {
        // Duvar kağıdı ayarlama işlemleri için enum
        public enum WallpaperTarget
        {
            HomeScreen,
            LockScreen,
            Both
        }

        public interface IWallpaperService
        {
            Task<bool> SetWallpaperAsync(byte[] imageBytes, WallpaperTarget target);
        }
    }
}
