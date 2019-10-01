using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;

namespace CustomerApi
{
    public class FakeApiUserTelemetryInitializer : TelemetryInitializerBase
    {
        public FakeApiUserTelemetryInitializer(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        protected override void OnInitializeTelemetry(HttpContext platformContext, RequestTelemetry requestTelemetry, ITelemetry telemetry)
        {
            var headers = platformContext.Request.Headers;
            if (headers.ContainsKey("x-fake-user"))
            {
                telemetry.Context.User.Id = headers["x-fake-user"];
            }
        }
    }
}
