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
	public class AssociationsController : Controller
	{
		ITagsService tagsService;
		public AssociationsController(ITagsService tagsService)
		{
			this.tagsService = tagsService;
		}
		// GET: api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public async Task<IEnumerable<TagAssociationContext>> Get(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return null;
			}
			var result = await tagsService.SearchTagAssociationsAsync(id);
			return result;
		}

		// POST api/values
		[HttpPost]
		public async Task Post([FromBody]TagAssociationDTO value)
		{
			await tagsService.Associate(value.Id, value.AssociatedObjectId, 0);
		}
	}
}
