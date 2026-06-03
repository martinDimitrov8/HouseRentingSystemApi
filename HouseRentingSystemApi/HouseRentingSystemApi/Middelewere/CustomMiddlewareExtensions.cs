
namespace HouseRentingSystemApi.Middelewere
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustom(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddelewere>();
        }

        public static IApplicationBuilder StopWatch(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseStopWatchMiddelware>();
        }
    }

}
