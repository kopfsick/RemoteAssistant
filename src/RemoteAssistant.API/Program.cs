using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;

namespace RemoteAssistant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = GetConfiguration(args);

            var port = config.GetValue("port", "5432");
            var runInConsole = config.GetValue("console", false) || Debugger.IsAttached;

            var webHost = CreateWebHostBuilder(args, port).Build();

            if(runInConsole)
                webHost.Run();
            else
                webHost.RunAsService();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, string port)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://*:{port}");
        }

        private static IConfigurationRoot GetConfiguration(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();
            return config;
        }
    }
}
