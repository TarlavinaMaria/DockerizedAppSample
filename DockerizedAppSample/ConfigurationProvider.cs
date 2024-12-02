using System.Runtime.InteropServices;

namespace DockerizedAppSample
{
    public class ConfigurationProvider
    {
        private static readonly IConfigurationRoot configuration;

        static ConfigurationProvider()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public static string? DbConnectionString
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    return configuration.GetConnectionString("win");
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return configuration.GetConnectionString("linux");
                }
                return null;
            }
        }
    }
}
