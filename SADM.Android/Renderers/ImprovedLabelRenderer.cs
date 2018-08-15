using System.ComponentModel;
using Android.Content;
using SADM.Controls;
using SADM.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImprovedLabel), typeof(ImprovedLabelRenderer))]
namespace SADM.Droid.Renderers
{
    public class ImprovedLabelRenderer : LabelRenderer
    {
        public ImprovedLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if(Element is ImprovedLabel improvedLabel)
            {
                Control.SetLines(improvedLabel.Lines);
                Control.Ellipsize = Android.Text.TextUtils.TruncateAt.End;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ImprovedLabel.LinesProperty.PropertyName)
            {
                if(sender is ImprovedLabel improvedLabel)
                {
                    Control.SetLines(improvedLabel.Lines);
                }

            }
        }
    }
}
