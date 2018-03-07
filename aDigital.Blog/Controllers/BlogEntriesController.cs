using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace aDigital.Blog.Controllers
{
	[Route("[controller]")]
	public class BlogEntriesController : ControllerFundation
	{
		IBlogServices _services;
		public BlogEntriesController(IBlogServices services) : base()
		{
			_services = services;
		}

		// GET api/values/5
		[HttpGet("{id?}")]
		public async Task<IEnumerable<IBlogEntry>> Get(int id)
		{
			return await _services.List(10, id);
		}
	}
}
