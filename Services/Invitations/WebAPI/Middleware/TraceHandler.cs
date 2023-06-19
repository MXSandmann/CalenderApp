using System.Diagnostics;

namespace WebAPI.Middleware
{
    public class TraceHandler
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly string _serviceName;

        public TraceHandler(RequestDelegate requestDelegate, string serviceName)
        {
            _requestDelegate = requestDelegate;
            _serviceName = serviceName;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var activitySource = new ActivitySource(_serviceName);
            using var activity = activitySource.StartActivity("Handle request");
            await _requestDelegate(httpContext);
        }
    }
}
