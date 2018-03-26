using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using aDigitalWebSite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace aDigitalWebSite.Web.Controllers
{
	public class PostsController : BaseController
	{
		IConfiguration _config;
		public PostsController(IConfiguration configuration)
		{
			_config = configuration;
		}

		//[Route("Posts/{id:int?}")]
		public async Task<IActionResult> Index(string id)
		{
			var client = new RestClient(_config["blogApiRootUrl"]);
			var request = new RestRequest("/blogentries/{id}", Method.GET);
			request.AddUrlSegment("id", id);
			var response = await client.ExecuteTaskAsync(request);
			var result = JsonConvert.DeserializeObject<IEnumerable<BlogEntryDTO>>(response.Content).FirstOrDefault();
			//result.SequentialId
			return View(result);
		}
	}
}
