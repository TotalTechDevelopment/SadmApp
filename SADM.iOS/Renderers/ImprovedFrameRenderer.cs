using CoreGraphics;
using SADM.Controls;
using SADM.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImprovedFrame), typeof(ImprovedFrameRenderer))]
namespace SADM.iOS.Renderers
{
    public class ImprovedFrameRenderer: FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.CornerRadius = 5;
            Layer.MasksToBounds = true;
            Layer.ShadowOffset = new CGSize(-2, 2);
            Layer.ShadowRadius = 5;
            Layer.ShadowOpacity = 0.4f;
        }
    }
}
