using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using SADM.Controls;
using SADM.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(InputBox), typeof(InputBoxRenderer))]
namespace SADM.Droid.Renderers
{
    /// <summary>
    /// Control for user inputs.
    /// </summary>
    public class InputBoxRenderer : FrameRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Droid.Renderers.InputBoxRenderer"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public InputBoxRenderer(Context context) : base(context)
        {
            //Is required for Xamarn.Forms 2.5 or higher
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.OldElement == null) {
                    Elevation = TypedValue.ApplyDimension(ComplexUnitType.Dip,
                                   Convert.ToSingle(10), Context.Resources.DisplayMetrics);
                Control.Radius = TypedValue.ApplyDimension(ComplexUnitType.Dip,
                                               Convert.ToSingle(15), Context.Resources.DisplayMetrics);
            }
        }
    }
}