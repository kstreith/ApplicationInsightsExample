using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.RegularExpressions;

namespace CustomerApi
{
    public class ScrubCustomerIdTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor? _next;
        private readonly IConfiguration? _config;
        private readonly Regex _guidRegex;

        public ScrubCustomerIdTelemetryProcessor(ITelemetryProcessor? next, IConfiguration? config)
        {
            _next = next;
            _config = config;
            _guidRegex = new Regex("([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
        }

        public void Process(ITelemetry? item)
        {
            try
            {
                if (_config.GetValue<bool>("ScrubCustomerId") == true)
                {
                    var request = item as RequestTelemetry;
                    if (request != null)
                    {
                        request.Name = _guidRegex.Replace(request.Name, "guid");
                        request.Url = new Uri(_guidRegex.Replace(request.Url.AbsoluteUri, "guid"));
                    }
                }
            }
            finally
            {
                _next?.Process(item);
            }
        }
    }
}
