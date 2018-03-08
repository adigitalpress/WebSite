using System;
using aDigitalWebSite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace aDigitalWebSite.Web.Controllers
{
	public class ErrorController : BaseController
	{
		public IActionResult Index(int id)
		{
			return View(id.ToString());
		}
	}
}
