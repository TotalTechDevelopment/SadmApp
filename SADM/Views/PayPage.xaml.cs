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
        void w_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Source")
            {
                var contentWebView = sender as WebView;
                var o = contentWebView.Source as UrlWebViewSource;
                if(o.Url.Contains("http://localhost:8080/api/"))
                {
                    _event.GetEvent<UrlChangeEvent>().Publish(o.Url);
                }
            }
        }
    }
}
