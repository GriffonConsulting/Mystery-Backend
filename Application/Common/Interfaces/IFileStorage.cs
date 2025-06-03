namespace Application.Common.Interfaces
{
    public interface IFileStorage
    {
        public Task UploadFileFromUrlAsync(string fileUrl, string obj);
        public Task<string> GetFileAsync(string obj);
    }
}