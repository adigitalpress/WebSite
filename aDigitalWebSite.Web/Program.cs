using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aDigitalWebSite.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost(args);
			host.Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
#if DEBUG
			return WebHost.CreateDefaultBuilder(args)
							  .UseApplicationInsights("77fec417-0965-4c97-868b-8237ceaa8b24")
							.UseStartup<Startup>()
							.UseKestrel()
					   		.UseUrls(new string[] { "http://0.0.0.0:5001", "http://10.0.0.225:5001" })
							.Build();
#else
			return WebHost.CreateDefaultBuilder(args)
					.UseApplicationInsights("77fec417-0965-4c97-868b-8237ceaa8b24")
					.UseStartup<Startup>()
					.UseKestrel()
						  .UseUrls(new string[] { "http://adigital.press" })
					.Build();
#endif

		}
	}
}
