using System.Globalization;

namespace SADM.Services
{
    /// <summary>
    /// Contract to Localize service.
    /// </summary>
    public interface ILocalizeService
    {
        /// <summary>
        /// Sets the locale.
        /// </summary>
        /// <param name="cultureInfo">Culture info.</param>
        void SetLocale(CultureInfo cultureInfo);

        /// <summary>
        /// Gets the current CultureInfo.
        /// </summary>
        /// <returns>The current culture info.</returns>
        CultureInfo GetCurrentCultureInfo();
    }
}
