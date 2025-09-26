using Android.Content;
using Android.OS;
using Android.Provider;
using WertinatWallify.Services;
using Environment = Android.OS.Environment;

namespace WertinatWallify.Platforms.Android
{
    public class PhotoSaverService : IPhotoSaverService
    {
        public async Task<bool> SavePhotoAsync(byte[] photoBytes, string fileName)
        {
            try
            {
                var context = Platform.AppContext;
                var contentValues = new ContentValues();
                contentValues.Put(MediaStore.IMediaColumns.DisplayName, fileName);
                contentValues.Put(MediaStore.IMediaColumns.MimeType, "image/jpeg");

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    contentValues.Put(MediaStore.IMediaColumns.RelativePath, Environment.DirectoryPictures);
                }

                var uri = context.ContentResolver.Insert(MediaStore.Images.Media.ExternalContentUri, contentValues);
                if (uri == null) return false;

                using var outputStream = context.ContentResolver.OpenOutputStream(uri);
                await outputStream.WriteAsync(photoBytes);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving photo to gallery: {ex.Message}");
                return false;
            }
        }
    }
}