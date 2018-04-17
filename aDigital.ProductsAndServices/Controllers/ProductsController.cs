using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.AspNetCore.Mvc;

namespace aDigital.ProductsAndServices.Controllers
{
	[Route("")]
	public class ProductsController : Controller
	{
		IProductServices _productServices;
		ILogger _logger;

		public ProductsController(IProductServices productServices, ILogger logger)
		{
			_productServices = productServices;
			_logger = logger;
		}

		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// PUT api/values/5
		[HttpPut]
		public async Task Put([FromBody]ProductDTO value)
		{
			await _productServices.CreateProduct(value);
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
