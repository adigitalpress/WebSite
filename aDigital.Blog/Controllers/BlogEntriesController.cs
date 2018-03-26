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
	public class BlogEntriesController : ControllerFundation
	{
		IBlogServices _services;
		public BlogEntriesController(IBlogServices services) : base()
		{
			_services = services;
		}

		// GET api/values/5
		[HttpGet("/blogEntries/{id?}")]
		public async Task<IEnumerable<IBlogEntry>> Get(string id)
		{
			IEnumerable<IBlogEntry> res = null;
			if (string.IsNullOrEmpty(id))
			{
				res = await _services.List(10, 0);
			}
			else
			{
				res = new List<IBlogEntry> { await _services.List(id) };
			}
			return res;
		}
	}
}
