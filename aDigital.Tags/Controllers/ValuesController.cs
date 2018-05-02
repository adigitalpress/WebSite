using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.AspNetCore.Mvc;

namespace aDigital.Tags.Controllers
{
	[Route("")]
	public class ValuesController : Controller
	{
		ITagsService tagsService;
		public ValuesController(ITagsService tagService)
		{
			this.tagsService = tagService;
		}
		// GET api/values
		[HttpGet]
		public async Task<IEnumerable<string>> Get()
		{
			return await tagsService.GetTags();
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
