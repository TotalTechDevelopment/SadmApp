using System.Globalization;
using System.Threading;
using SADM.Droid.Services;
using SADM.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalizeService))]
namespace SADM.Droid.Services
{
    /// <summary>
    /// Localize service.
    /// </summary>
    public class LocalizeService : ILocalizeService
    {
        /// <summary>
        /// Sets the locale.
        /// </summary>
        /// <param name="cultureInfo">Culture info.</param>
        public void SetLocale(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        /// <summary>
        /// Gets the current culture info.
        /// </summary>
        /// <returns>The current CultureInfo.</returns>
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = AndroidToDotnetLanguage(androidLocale.ToString().Replace("_", "-"));
            CultureInfo cultureInfo = null;
            try
            {
                cultureInfo = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    cultureInfo = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
                    cultureInfo = new CultureInfo("en");
                }
            }
            return cultureInfo;
        }

        /// <summary>
        /// Android language to dotnet language.
        /// </summary>
        /// <returns>The to dotnet language.</returns>
        /// <param name="androidLanguage">Android language.</param>
        protected string AndroidToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "ms-BN":   // "Malaysian (Brunei)" not supported .NET culture
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "in-ID":  // "Indonesian (Indonesia)" has different code in  .NET
                    netLanguage = "id-ID"; // correct code for .NET
                    break;
                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
            }
            return netLanguage;
        }

        /// <summary>
        /// Tos the dotnet fallback language.
        /// </summary>
        /// <returns>The dotnet fallback language.</returns>
        /// <param name="platCulture">Plat culture.</param>
        protected string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);
            switch (platCulture.LanguageCode)
            {
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
            }
            return netLanguage;
        }
    }
}
