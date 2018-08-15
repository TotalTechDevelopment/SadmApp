using System;
namespace SADM.Services
{
    /// <summary>
    /// Platform culture.
    /// </summary>
    public class PlatformCulture
    {
        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocaleCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SADM.Services.PlatformCulture"/> class.
        /// </summary>
        /// <param name="platformCultureString">Platform culture string.</param>
        public PlatformCulture(string platformCultureString)
        {
            if (String.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", nameof(platformCultureString)); // in C# 6 use nameof(platformCultureString)
            }
            PlatformString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = string.Empty;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:SADM.Services.PlatformCulture"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:SADM.Services.PlatformCulture"/>.</returns>
        public override string ToString()
        {
            return PlatformString;
        }
    }
}
