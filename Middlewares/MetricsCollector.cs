using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Prometheus;
using Microsoft.AspNetCore.Authentication;

namespace com.b_velop.stack.GraphQl.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MetricsCollector
    {
        private readonly RequestDelegate _next;
        public static Gauge Gauge = Metrics.CreateGauge(
            "b_velop_stack_datalayer_requests_duration",
            "duration of requests",
            new GaugeConfiguration
            {
                LabelNames = new[] { "Path", "Method" }
            });

        public static Counter Counter = Metrics.CreateCounter(
            "b_velop_stack_datalayer_requests_total",
            "duration of requests",
            new CounterConfiguration
            {
                LabelNames = new[] { "Path", "Method" }
            });

        public MetricsCollector(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            Counter.WithLabels(httpContext.Request.Path, httpContext.Request.Method).Inc();
            using (Gauge.WithLabels(httpContext.Request.Path, httpContext.Request.Method).NewTimer())
            {
                if (httpContext.Request.Method == "POST")
                {
                    var token = httpContext.AuthenticateAsync("Bearer").Result;
                    if (token.Succeeded)
                        System.Console.WriteLine("hey");
                    else
                    {
                        return httpContext.ForbidAsync();
                    }
                }
                return _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MetricsCollectorExtensions
    {
        public static IApplicationBuilder UseMetricsCollector(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MetricsCollector>();
        }
    }
}
