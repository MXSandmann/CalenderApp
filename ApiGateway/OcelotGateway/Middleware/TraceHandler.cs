using System.Diagnostics;

namespace OcelotGateway.Middleware
{
    public class TraceHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public TraceHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var activitySource = new ActivitySource("Ocelot API Gateway");
            using var activity = activitySource.StartActivity("Reroute request");
            await _requestDelegate(httpContext);
        }
    }
}