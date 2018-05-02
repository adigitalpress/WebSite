using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigitalWebSite.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aDigitalWebSite.Web.Controllers
{
	public class ServicesController : BaseController
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}

		public async Task<IActionResult> Preview()
		{
			return View();
		}
	}
}
