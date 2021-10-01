using Microsoft.Extensions.Configuration;
using System.IO;

namespace USSDTELCOCharge.Data.Helpers
{
    public class ConfigSettings
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder();
        static IConfigurationRoot _ConfigRoot;
        private static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);

            return appRoot;
        }
        static private IConfigurationRoot GetAppSettings()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(ApplicationExeDirectory())
            .AddJsonFile("appsettings.json");
            if (_ConfigRoot == null)
            {
                _ConfigRoot = builder.Build();
            }
            return _ConfigRoot;
        }

        public static string chargesexempt_DepCode => GetAppSettings()["AppSetting:chargesexempt_DepCode"];
        public static string purchasesdbcon => GetAppSettings()["ConnectionStrings:purchasesconn"];
        public static string lubredUserName => GetAppSettings()["lubredUserName"];
        public static string lubredToken => GetAppSettings()["lubredToken"];
        public static string currentNetWorkUrl => GetAppSettings()["currentNetWorkUrl"];

    }
}
