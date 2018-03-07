using System;
using Microsoft.AspNetCore.Mvc;

namespace aDigitalWebSite.Web.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult Index(int id)
		{
			return View(id.ToString());
		}
	}
}
