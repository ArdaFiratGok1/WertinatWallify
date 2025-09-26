namespace WertinatWallify.Services
{
    public interface IPhotoSaverService
    {
        Task<bool> SavePhotoAsync(byte[] photoBytes, string fileName);
    }
}