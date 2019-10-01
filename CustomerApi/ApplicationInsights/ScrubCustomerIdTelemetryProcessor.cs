﻿using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Text.RegularExpressions;

namespace CustomerApi
{
    public class ScrubCustomerIdTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;
        private readonly Regex _guidRegex;

        public ScrubCustomerIdTelemetryProcessor(ITelemetryProcessor next)
        {
            _next = next;
            _guidRegex = new Regex("([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
        }

        public void Process(ITelemetry item)
        {
            try
            {
                var request = item as RequestTelemetry;
                if (request != null)
                {
                    request.Url = new Uri(_guidRegex.Replace(request.Url.AbsoluteUri, "guid"));
                }
            }
            finally
            {
                _next.Process(item);
            }
        }
    }
}