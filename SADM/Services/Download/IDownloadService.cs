namespace SADM.Services
{
    public interface IDownloadService
    {
        void DownloadFile(string uri, string fileName);
    }
}