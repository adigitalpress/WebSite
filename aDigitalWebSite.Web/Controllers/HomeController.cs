using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aDigitalWebSite.Web.Models;
using aDigitalWebSite.Web.Infrastructure;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace aDigitalWebSite.Web.Controllers
{
	public class HomeController : BaseController
	{
		IConfiguration _config;
		public HomeController(IConfiguration config)
		{
			_config = config;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		[HttpPost]
		public async Task Contact([FromBody]ContactFormDTO content)
		{
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential(_config["emailAccount"], _config["emailPassword"]);

			MailMessage mailMessage = new MailMessage();
			mailMessage.From = new MailAddress("donotreply@adigital.press");
			mailMessage.To.Add("contato@adigital.press");
			mailMessage.Body = content.Message;
			mailMessage.Subject = content.Email + " - EMAIL DE CONTATO DO SITE";
			await client.SendMailAsync(mailMessage);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult NewFeatures()
		{
			return View();
		}
	}

	public class ContactFormDTO
	{
		public string Message { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
	}
}