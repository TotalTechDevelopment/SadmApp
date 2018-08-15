using System.ComponentModel;
using SADM.Controls;
using SADM.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImprovedEditor), typeof(ImprovedEditorRenderer))]
namespace SADM.iOS.Renderers
{
    public class ImprovedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            Control.Editable = false;
            if (Element is ImprovedEditor improvedEditor)
            {
                improvedEditor.IsEnabled = false;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control.Editable = false;
            if (Element is ImprovedEditor improvedEditor)
            {
                improvedEditor.IsEnabled = false;
            }
        }
    }
}