
using System;
using com.b_velop.stack.DataContext.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Prometheus;

namespace com.b_velop.stack.GraphQl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stage = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            string file;
            if (stage == "Development")
                file = "nlog-dev.config";
            else
                file = "nlog.config";

            var logger = NLogBuilder.ConfigureNLog(file).GetCurrentClassLogger();
            try
            {
                if (stage != "Development")
                {
                    var metricServer = new MetricPusher(
                        endpoint: "https://push.qaybe.de/metrics",
                        job: "stack_graphql");
                    metricServer.Start();
                }

                logger.Debug("init main");
                CreateHostBuilder(args)
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
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.UseUrls("http://*:3000");
                      webBuilder.UseStartup<Startup>();
                  })
                .ConfigureServices(services =>
                {
                    var conString = Environment.GetEnvironmentVariable("ConString");
#if DEBUG
                    conString = "Server=localhost,1433;Database=Measure;User Id=sa;Password=foo123bar!";
#endif
                    services.AddDbContext<MeasureContext>(options =>
                    {
                        options.EnableDetailedErrors(true);
                        options.EnableSensitiveDataLogging(true);
                        options.UseSqlServer(conString, b => b.MigrationsAssembly("Stack.GraphQL"));
                    });
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
