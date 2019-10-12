using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using System;

namespace CustomerApi
{
    public class FakeApiUserTelemetryInitializer : TelemetryInitializerBase
    {
        public FakeApiUserTelemetryInitializer(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        protected override void OnInitializeTelemetry(HttpContext platformContext, RequestTelemetry requestTelemetry, ITelemetry telemetry)
        {
            if (platformContext == null)
            {
                throw new ArgumentNullException(nameof(platformContext));
            }
            if (telemetry == null)
            {
                throw new ArgumentNullException(nameof(telemetry));
            }
            var headers = platformContext.Request.Headers;
            if (headers.ContainsKey("x-fake-user"))
            {
                telemetry.Context.User.Id = headers["x-fake-user"];
            }
        }
    }
}
