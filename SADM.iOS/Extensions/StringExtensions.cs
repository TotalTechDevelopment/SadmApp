using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SADM.iOS.Extensions
{
    /// <summary>
    /// String extensions for iOS.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets height from a string.
        /// </summary>
        /// <returns>The height string.</returns>
        /// <param name="text">Text.</param>
        /// <param name="font">Font.</param>
        /// <param name="width">Width.</param>
        public static nfloat StringHeight(this string text, UIFont font, nfloat width)
        {
            return text.StringRect(font, width).Height;
        }

        /// <summary>
        /// Convert string to NSString.
        /// </summary>
        /// <returns>The NSString.</returns>
        /// <param name="text">The string.</param>
        public static NSString ToNativeString(this string text)
        {
            return new NSString(text);
        }

        /// <summary>
        /// Gets CGRect from a text.
        /// </summary>
        /// <returns>The rect.</returns>
        /// <param name="text">Text.</param>
        /// <param name="font">Font.</param>
        /// <param name="width">Width.</param>
        public static CGRect StringRect(this string text, UIFont font, nfloat width)
        {
            return text.ToNativeString()
                       .GetBoundingRect(new CGSize(width, nfloat.MaxValue),
                                        NSStringDrawingOptions.OneShot,
                                        new UIStringAttributes { Font = font },
                                        null);
        }
    }
}
