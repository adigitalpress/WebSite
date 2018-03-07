using System;
using Microsoft.AspNetCore.Mvc;

namespace aDigitalWebSite.Web.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
