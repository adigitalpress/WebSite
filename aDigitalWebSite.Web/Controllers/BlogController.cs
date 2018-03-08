using System;
using aDigitalWebSite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace aDigitalWebSite.Web.Controllers
{
	public class BlogController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
