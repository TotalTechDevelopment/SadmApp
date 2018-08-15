using Android.Content;
using SADM.Controls;
using SADM.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImprovedEditor), typeof(ImprovedEditorRenderer))]
namespace SADM.Droid.Renderers
{
    public class ImprovedEditorRenderer : EditorRenderer
    {
        public ImprovedEditorRenderer(Context context) : base(context)
        {
            //Is required for Xamarn.Forms 2.5 or higher
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.Focusable = false;
            }
        }
    }
}