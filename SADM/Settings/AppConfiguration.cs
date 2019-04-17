#define Develop
//#define Production

using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SADM.Models.Settings;

namespace SADM.Settings
{
    public class AppConfiguration
    {
        public static ConfigurationValue Values { get; } = Initialize();

        private static ConfigurationValue Initialize()
        {
            var assembly = typeof(AppConfiguration).GetTypeInfo().Assembly;
#if Develop
            var stream = assembly.GetManifestResourceStream("SADM.Settings.Development.json");
#elif Production
            var stream = assembly.GetManifestResourceStream("SADM.Settings.Production.json");
#endif
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<ConfigurationValue>(reader.ReadToEnd());
            }
        }
    }
}