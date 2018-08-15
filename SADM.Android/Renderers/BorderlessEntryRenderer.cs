using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using SADM.Controls;
using SADM.Droid.Renderers;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace SADM.Droid.Renderers
{
    /// <summary>
    /// Entry without borders.
    /// </summary>
    public class BorderlessEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Droid.Renderers.BorderlessEntryRenderer"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public BorderlessEntryRenderer(Context context) : base(context)
        {
            //Is required for Xamarn.Forms 2.5 or higher
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e?.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}
