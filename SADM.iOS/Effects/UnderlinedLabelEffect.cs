using System;
using Foundation;
using SADM.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("sadm")]
[assembly: ExportEffect(typeof(UnderlinedLabelEffect), nameof(UnderlinedLabelEffect))]
namespace SADM.iOS.Effects
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
        private void SetUnderline(bool underlined)
        {
            try
            {
                if(Control is UILabel label && label.AttributedText is NSMutableAttributedString text)
                {
                    var range = new NSRange(0, text.Length);
                    if (underlined)
                    {
                        text.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
                    }
                    else
                    {
                        text.RemoveAttribute(UIStringAttributeKey.UnderlineStyle, range);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot underline Label. Error: {ex.Message}");
            }
        }
    }
}
