using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace RedOnion.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
                .ConfigureLogging(logger =>
                {
                    logger.AddApplicationInsights();
                    logger.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
                    logger.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
                }).UseApplicationInsights()
            .Build();
            
    }
}
