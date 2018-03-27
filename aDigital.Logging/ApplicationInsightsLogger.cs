using System;
using aDigital.Library;
using Microsoft.ApplicationInsights;
using DC = Microsoft.ApplicationInsights.DataContracts;

namespace aDigital.Logging
{
	public class ApplicationInsightsLogger : ILogger
	{
		TelemetryClient client;

		public ApplicationInsightsLogger()
		{
			client = new TelemetryClient();
		}

		public void Log(ITelemetry telemetry)
		{
			DC.TraceTelemetry trace = new DC.TraceTelemetry();
			trace.Message = (telemetry as TraceTelemetry)?.Message;
			foreach (var item in telemetry.Properties)
			{
				trace.Properties.Add(item.Key, item.Value);
			}
			client.TrackTrace(trace);
		}

		public void LogDependency(ITelemetry telemetry)
		{
			throw new NotImplementedException();
		}

		public void LogException(ITelemetry telemetry)
		{
			DC.ExceptionTelemetry ex = new DC.ExceptionTelemetry();
			ex.Exception = (telemetry as ExceptionTelemetry)?.Exception;
			foreach (var item in telemetry.Properties)
			{
				ex.Properties.Add(item.Key, item.Value);
			}
			client.TrackException(ex);
		}
	}
}
