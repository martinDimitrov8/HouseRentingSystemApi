using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HouseRentingSystemApi.Middelewere
{
    public class UseStopWatchMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UseStopWatchMiddelware> _logger;

        public UseStopWatchMiddelware(RequestDelegate next, ILogger<UseStopWatchMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sv = new Stopwatch();
           sv.Start();
            
            
            await _next(context);
            sv.Stop();

            Console.WriteLine($"The resutl is {((int)sv.Elapsed.TotalMilliseconds),2} ms.");



        }
    }

    public static class UseStopWatchMiddelwareExtensions
    {
        public static IApplicationBuilder UseStopWatch(this IApplicationBuilder app)
            => app.UseMiddleware<UseStopWatchMiddelware>();
    }
}
