using SADM.Droid.Services;
using SADM.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DownloadService))]
namespace SADM.Droid.Services
{
    public class DownloadService : IDownloadService
    {
        public void DownloadFile(string uri, string fileName)
        {
            var contentUri = Android.Net.Uri.Parse(uri);
            var request = new Android.App.DownloadManager.Request(contentUri);
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, fileName);
            request.AllowScanningByMediaScanner();
            request.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);
            var downloadManager = (Android.App.DownloadManager)Forms.Context.GetSystemService(Android.Content.Context.DownloadService);
            downloadManager.Enqueue(request);
        }
    }
}