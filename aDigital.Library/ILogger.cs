using System;
using System.Collections.Generic;

namespace aDigital.Library
{
	public interface ILogger
	{
		void Log(ITelemetry telemetry);
		void LogDependency(ITelemetry telemetry);
		void LogException(ITelemetry telemetry);
	}

	public interface ITelemetry
	{
		IDictionary<string, string> Properties { get; set; }

	}

	public class BaseTelemetry : ITelemetry
	{
		public BaseTelemetry()
		{
			Properties = new Dictionary<string, string>();
		}
		public IDictionary<string, string> Properties { get; set; }
		public int Elapsed
		{
			get
			{
				string aux = null;
				Properties.TryGetValue("elapsed", out aux);
				return int.Parse(aux);
			}
			set
			{
				Properties["elapsed"] = value.ToString();
			}
		}
	}

	public class TraceTelemetry : BaseTelemetry
	{
		public string Message { get; set; }

	}

	public class ExceptionTelemetry : BaseTelemetry
	{
		public Exception Exception { get; set; }
	}
}
