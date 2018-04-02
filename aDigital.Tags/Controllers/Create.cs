using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aDigital.Tags.Controllers
{
	[Route("[controller]")]
	public class Create : Controller
	{
		ILogger logger;
		ITagsService tagsService;
		public Create(ILogger logger, ITagsService tagsService)
		{
			this.logger = logger;
			this.tagsService = tagsService;
		}
		// PUT api/values/5
		[HttpPut("{id}")]
		public async Task Put(string id)
		{
			await tagsService.CreateTag(id);
		}
	}
}
