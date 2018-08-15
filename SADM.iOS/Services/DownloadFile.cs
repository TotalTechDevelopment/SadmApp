using Android.App;
using Xamarin.Forms;

[assembly: Dependency(typeof(dowloadfile.Droid.DownloadFile))]
namespace dowloadfile.Droid
{
    public class DownloadFile : IDownloadManager
    {
        public static void Init()
        {
        }
        public void Download(string uri)
        {
            string filename = "";
            var validate = uri;
            if(uri.Contains("idpdf"))
            {
                var name = uri.Replace("http://www.sadm.gob.mx/Solicitudes/solicitudcfdi?idpdf=", "");
                filename = name.Replace("/","") + ".pdf";
            }
            else
            {
                var name = uri.Replace("http://www.sadm.gob.mx/Solicitudes/solicitudcfdi?idxml=", "");
                filename = name.Replace("/","") + ".xml";
            }
            Android.Net.Uri contentUri = Android.Net.Uri.Parse(uri);

            DownloadManager.Request r = new DownloadManager.Request(contentUri);


            r.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, filename);

            r.AllowScanningByMediaScanner();

            r.SetNotificationVisibility(Android.App.DownloadVisibility.VisibleNotifyCompleted);

            DownloadManager dm = (DownloadManager)Forms.Context.GetSystemService(Android.Content.Context.DownloadService);

            dm.Enqueue(r);
        }
    }
}
