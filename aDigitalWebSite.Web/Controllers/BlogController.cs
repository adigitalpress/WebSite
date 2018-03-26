using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aDigital.Library;
using aDigitalWebSite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

namespace aDigitalWebSite.Web.Controllers
{
	public class BlogController : BaseController
	{
		IConfiguration _config;
		public BlogController(IConfiguration configs)
		{
			_config = configs;
		}
		public async Task<IActionResult> Index()
		{
			var client = new RestClient(_config["blogApiRootUrl"]);
			var request = new RestRequest("/blogentries", Method.GET);

			var response = await client.ExecuteTaskAsync(request);
			var result = JsonConvert.DeserializeObject<IEnumerable<BlogEntryDTO>>(response.Content);
			return View(result);
		}
	}
}
