using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace com.b_velop.stack.GraphQl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stage = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var file = string.Empty;

            if (stage == "Development")
                file = "nlog-dev.config";
            else
                file = "nlog.config";

            var logger = NLogBuilder.ConfigureNLog(file).GetCurrentClassLogger();
            try
            {
                if (stage != "Development")
                {
                    //var metricServer = new MetricPusher(
                    //    endpoint: "https://push.qaybe.de/metrics",
                    //    job: "stack_graphql");
                    //metricServer.Start();
                }

                logger.Debug("init main");
                CreateWebHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:3000")
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
