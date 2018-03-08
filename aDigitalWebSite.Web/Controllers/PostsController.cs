using System;
using System.Threading.Tasks;
using aDigitalWebSite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace aDigitalWebSite.Web.Controllers
{
	public class PostsController : BaseController
	{
		public PostsController()
		{
		}

		//[Route("Posts/{id:int?}")]
		public async Task<IActionResult> Index(string id)
		{
			int intId = 0;
			if (!int.TryParse(id, out intId) || intId > 2)
			{
				return StatusCode(404);
			}
			return View("Post" + id);
		}
	}
}
