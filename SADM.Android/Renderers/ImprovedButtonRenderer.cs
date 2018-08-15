using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using SADM.Controls;
using SADM.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImprovedButton), typeof(ImprovedButtonRenderer))]
namespace SADM.Droid.Renderers
{
    /// <summary>
    /// Custom Button for the App.
    /// </summary>
	public class ImprovedButtonRenderer : ButtonRenderer
    {
        protected GradientDrawable gradientBackground;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Droid.Renderers.ImprovedButtonRenderer"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public ImprovedButtonRenderer(Context context) : base(context)
        {
            //Is required for Xamarn.Forms 2.5 or higher
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Paint();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ImprovedButton.ImprovedBorderColorProperty.PropertyName ||
                e.PropertyName == ImprovedButton.ImprovedBorderRadiusProperty.PropertyName ||
                e.PropertyName == ImprovedButton.ImprovedBorderWidthProperty.PropertyName)
            {
                Paint();
            }
        }

        /// <summary>
        /// Refresh view.
        /// </summary>
        protected void Paint()
        {
            if (Element is ImprovedButton button)
            {
                gradientBackground = new GradientDrawable();
                gradientBackground.SetShape(ShapeType.Rectangle);
                gradientBackground.SetColor(button.ImprovedBackgroundColor.ToAndroid());
                // Thickness of the stroke line  
                gradientBackground.SetStroke((int)button.ImprovedBorderWidth, button.ImprovedBorderColor.ToAndroid());
                // Radius for the curves
                var borderPixels = TypedValue.ApplyDimension(ComplexUnitType.Dip, 
                                   Convert.ToSingle(button.ImprovedBorderRadius),Context.Resources.DisplayMetrics);
                gradientBackground.SetCornerRadius(borderPixels);
                // set the background of the label  
                Control.SetBackground(gradientBackground);
                Control.SetLines(2);
            }
        }
    }
}
