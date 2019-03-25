using System;
using System.Diagnostics;
using Plugin.Geolocator;
using Xamarin.Forms.GoogleMaps;

namespace SADM.Views
{
    public partial class GenerateReportPage : PageBase
    {

        private readonly static string TAG = nameof(GenerateReportPage);

        public GenerateReportPage()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(timeout:TimeSpan.FromSeconds(10));
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                             Distance.FromMiles(1)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message,TAG);
            }
        }
    }
}
