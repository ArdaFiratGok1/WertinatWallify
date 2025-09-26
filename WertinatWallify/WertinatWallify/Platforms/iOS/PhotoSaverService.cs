using Photos;
using UIKit;
using WertinatWallify.Services;

namespace WertinatWallify.Platforms.iOS
{
    public class PhotoSaverService : IPhotoSaverService
    {
        public async Task<bool> SavePhotoAsync(byte[] photoBytes, string fileName)
        {
            try
            {
                var image = new UIImage(Foundation.NSData.FromArray(photoBytes));
                var status = await PHPhotoLibrary.RequestAuthorizationAsync();
                if (status != PHAuthorizationStatus.Authorized)
                {
                    return false;
                }

                var tcs = new TaskCompletionSource<bool>();
                image.SaveToPhotosAlbum((img, error) => {
                    tcs.SetResult(error == null);
                });
                return await tcs.Task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving photo to gallery: {ex.Message}");
                return false;
            }
        }
    }
}