using SADM.Controls;
using SADM.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImprovedListView), typeof(ImprovedListViewRenderer))]
namespace SADM.iOS.Renderers
{
    public class ImprovedListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            Control.Bounces = false;
        }
    }
}
