using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using ServerMonitor.Memory;
using ServerMonitor.Ports;

namespace ServerMonitor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<MemoryMetricsClientLinux>();

            if (IsUnix())
            {
                services.AddTransient<IMemoryMetricsClient, MemoryMetricsClientLinux>();
            }
            else
            {
                services.AddTransient<IMemoryMetricsClient, MemoryMetricsClientWindows>();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // without that, nginx forwarding in docker wont work
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();
            app.UseCors(builder =>
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static bool IsUnix()
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                         RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            return isUnix;
        }
    }
}