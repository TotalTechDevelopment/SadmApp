using System.ComponentModel;
using System.Diagnostics;
using Prism.Events;
using SADM.Events;
using SADM.ViewModels;
using Xamarin.Forms;

namespace SADM.Views
{
    public partial class PayPage : PageBase
    {
        private IEventAggregator _event;

        public PayPage(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _event = eventAggregator;
        }

        async void w_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Source")
            {
                try
                {
                    var contentWebView = sender as WebView;
                    var o = contentWebView.Source as UrlWebViewSource;
                    var a = await contentWebView.EvaluateJavaScriptAsync("document.body.innerHTML");
                    if (a != null && a.Contains("HTTP Status - 400") || o.Url.Contains("op=cancel"))
                    {
                        _event.GetEvent<UrlChangeEvent>().Publish("back");
                    }
                    else if (o.Url.Contains("http://spartane.com/pay"))
                    {
                        webview.Source = "about:blank";
                        webview.IsVisible = false;
                        _event.GetEvent<UrlChangeEvent>().Publish(o.Url);
                    }
                }
                catch (System.Exception ex)
                {

                }

            }
        }
    }
}
