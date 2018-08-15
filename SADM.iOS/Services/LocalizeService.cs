using System.Globalization;
using System.Threading;
using Foundation;
using SADM.iOS.Services;
using SADM.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalizeService))]
namespace SADM.iOS.Services
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
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = DotnetLanguageFromiOS(pref);
            }
            CultureInfo cultureInfo = null;
            try
            {
                cultureInfo = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    cultureInfo = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
                    // iOS language not valid .NET culture, falling back to English
                    cultureInfo = new CultureInfo("en");
                }
            }
            return cultureInfo;
        }

        /// <summary>
        /// Gets Dotnet language from iOS.
        /// </summary>
        /// <returns>DotNet Language.</returns>
        /// <param name="iOSLanguage">iOS Language.</param>
        protected string DotnetLanguageFromiOS(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (iOSLanguage)
            {
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
            }
            return netLanguage;
        }

        /// <summary>
        /// Gets the dotnet fallback language.
        /// </summary>
        /// <returns>The dotnet fallback language.</returns>
        /// <param name="platformCulture">PlatformCulture</param>
        protected string ToDotnetFallbackLanguage(PlatformCulture platformCulture)
        {
            var netLanguage = platformCulture.LanguageCode;
            switch (platformCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
                    break;
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
            }
            return netLanguage;
        }
    }
}
