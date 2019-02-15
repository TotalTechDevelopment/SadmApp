using System;
using System.IO;
using System.Linq;
using System.Net;
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

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var library = Path.Combine(documents, "..", "Library");
            var localPath = Path.Combine(library, fileName);
            webClient.DownloadDataCompleted += (s, e) =>
            {
                File.WriteAllBytes(localPath, e.Result);
            };
            webClient.DownloadDataAsync(new Uri(uri));
        }
    }
}