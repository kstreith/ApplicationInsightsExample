using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace CustomerApi
{
    public class CustomTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;

        public CustomTelemetryProcessor(ITelemetryProcessor next)
        {
            _next = next;
        }

        public void Process(ITelemetry item)
        {
            try
            {
                var request = item as RequestTelemetry;
                if (request != null)
                {
                    request.Properties["wentThroughCustomProcessor"] = "true";
                }
                var trace = item as TraceTelemetry;
                if (trace != null)
                {
                    trace.Properties["customProp"] = "customValue";
                }
            }
            finally
            {
                _next.Process(item);
            }
        }
    }
}
