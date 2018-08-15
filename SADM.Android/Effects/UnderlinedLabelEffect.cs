using System;
using Android.Graphics;
using Android.Widget;
using SADM.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ResolutionGroupName("sadm")] 
[assembly: ExportEffect(typeof(UnderlinedLabelEffect), nameof(UnderlinedLabelEffect))] 
namespace SADM.Droid.Effects
{
    /// <summary>
    /// Underlined label effect.
    /// </summary>
    public class UnderlinedLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            SetUnderline(true);
        }

        protected override void OnDetached()
        {
            SetUnderline(false);
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                SetUnderline(true);
            }
        }

        /// <summary>
        /// Sets the underline.
        /// </summary>
        /// <param name="underlined">If set to <c>true</c> underlined.</param>
        protected void SetUnderline(bool underlined)
        {
            try
            {
                var textView = (TextView)Control;
                if (underlined)
                {
                    textView.PaintFlags |= PaintFlags.UnderlineText;
                }
                else
                {
                    textView.PaintFlags &= ~PaintFlags.UnderlineText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot underline Label. Error: {ex.Message}");
            }
        }

    }
}
