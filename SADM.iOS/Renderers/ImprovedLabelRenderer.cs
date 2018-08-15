using System.ComponentModel;
using SADM.Controls;
using SADM.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImprovedLabel), typeof(ImprovedLabelRenderer))]
namespace SADM.iOS.Renderers
{
    public class ImprovedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            RefreshProperties();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ImprovedLabel.LinesProperty.PropertyName)
            {
                RefreshProperties();
            }
        }

        protected void RefreshProperties()
        {
            if (Element is ImprovedLabel improvedLabel)
            {
                Control.Lines = improvedLabel.Lines;
                Control.LineBreakMode = UIKit.UILineBreakMode.TailTruncation;
            }
        }
    }
}