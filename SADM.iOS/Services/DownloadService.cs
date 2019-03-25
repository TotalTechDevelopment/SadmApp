using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Foundation;
using SADM.iOS.Services;
using SADM.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DownloadService))]
namespace SADM.iOS.Services
{
    public class DownloadService : IDownloadService
    {
        public void DownloadFile(string uri, string fileName)
        {
            var webClient = new WebClient();

            webClient.DownloadDataCompleted += (s, e) =>
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var filename = Path.Combine(documents, fileName);
                File.WriteAllBytes(filename, e.Result);
                Device.BeginInvokeOnMainThread(() => {
                    new UIAlertView("Éxito", "Archivo descargado correctamente.", null, "OK", null).Show();
                });
            };
            webClient.DownloadDataAsync(new Uri(uri));
        }
    }
}