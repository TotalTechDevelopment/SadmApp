using System;
using System.IO;
using System.Net;
using SADM.iOS.Services;
using SADM.Services;
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
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localPath = Path.Combine(documentsPath, fileName);
                File.WriteAllBytes(localPath, e.Result);   
            };
            webClient.DownloadDataAsync(new Uri(uri));
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");

        }
    }
}