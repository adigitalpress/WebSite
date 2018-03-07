using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aDigitalWebSite.Web.Controllers
{
	public class PostsController : Controller
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
