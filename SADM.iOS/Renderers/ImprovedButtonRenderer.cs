using System.ComponentModel;
using SADM.Controls;
using SADM.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImprovedButton), typeof(ImprovedButtonRenderer))]
namespace SADM.iOS.Renderers
{
    /// <summary>
    /// Custom Button for the App.
    /// </summary>
    public class ImprovedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Paint();
        }
  
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ImprovedButton.ImprovedBorderRadiusProperty.PropertyName ||
                e.PropertyName == ImprovedButton.ImprovedBorderColorProperty.PropertyName||
                e.PropertyName == ImprovedButton.ImprovedBorderWidthProperty.PropertyName )
            {
                Paint();
            }
        }

        /// <summary>
        /// Refresh button.
        /// </summary>
        protected void Paint()
        {
            if (Element is ImprovedButton button)
            {
                Layer.CornerRadius = (float)button.ImprovedBorderRadius;
                Layer.BorderColor = button.ImprovedBorderColor.ToCGColor();
                Layer.BackgroundColor = button.ImprovedBackgroundColor.ToCGColor();
                Layer.BorderWidth = (int)button.ImprovedBorderWidth;
                Control.TitleLabel.Lines = 2;
                Control.TitleLabel.TextAlignment = UIKit.UITextAlignment.Center;
            }
        }  
    }
}
