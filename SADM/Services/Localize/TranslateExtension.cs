using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SADM.Services
{
    /// <summary>
    /// Translate extension.
    /// </summary>
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        protected readonly CultureInfo cultureInfo = null;
        protected const string ResourceId = "SADM.AppResources";

        protected static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() =>
          new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Services.TranslateExtension"/> class.
        /// </summary>
        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                cultureInfo = DependencyService.Get<ILocalizeService>().GetCurrentCultureInfo();
            }
        }

        /// <summary>
        /// Provides the value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="serviceProvider">Service provider.</param>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            var translation = ResMgr.Value.GetString(Text, cultureInfo);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", 
                                  Text, 
                                  ResourceId, 
                                  cultureInfo.Name),
                    nameof(Text));
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
