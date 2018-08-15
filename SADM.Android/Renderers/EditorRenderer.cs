using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(SADM.Droid.Renderers.EditorRenderer))]

namespace SADM.Droid.Renderers
{
    public class EditorRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public EditorRenderer(Context context) : base(context)
        {
            //Is required for Xamarn.Forms 2.5 or higher
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e?.OldElement == null)
            {
                Control.Background = null;
                Control.SetBackgroundColor(Color.Transparent.ToAndroid());

            }
        }
    }
}
