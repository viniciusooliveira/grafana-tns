using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;

namespace OtelApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddOpenTelemetry(o =>
                    {

                        o.IncludeFormattedMessage = true;
                        o.IncludeScopes = true;
                        o.ParseStateValues = true;
                        o.AddOtlpExporter(otlp =>
                        {
                            otlp.ExportProcessorType = ExportProcessorType.Simple;
                            otlp.Endpoint = new Uri("http://collector:4317");
                        });
                    });
                    
                    // logging.AddConsole();
                })
                .Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
