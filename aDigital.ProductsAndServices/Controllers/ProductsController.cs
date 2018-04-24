using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using aDigital.Library.ProductsAndServices;
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
		public async Task<IEnumerable<ProductQueryDTO>> Get()
		{
			var products = await _productServices.GetProducts();
			var dtos = MapIProductToDTO(products);
			return dtos;
		}

		// GET /5
		[HttpGet("{id:int}")]
		public async Task<IEnumerable<ProductQueryDTO>> Get(int id)
		{
			var res = await _productServices.GetProductById(id);
			var products = MapIProductToDTO(res);
			return products;
		}

		// GET /Lona
		[HttpGet("{search}")]
		public async Task<IEnumerable<ProductQueryDTO>> Get(string search)
		{
			var res = await _productServices.SearchProduct(search);
			var products = MapIProductToDTO(res);
			return products;
		}

		private static IEnumerable<ProductQueryDTO> MapIProductToDTO(IEnumerable<IProduct> res)
		{
			return res.Select((i) => new ProductQueryDTO
			{
				Id = i.Id,
				Images = i.Images,
				StartsAt = i.StartsAt,
				Title = i.Title,
				Description = i.Description
			});
		}

		// PUT api/values/5
		[HttpPut]
		public async Task Put([FromBody]ProductCreationDTO value)
		{
			var dto = new ProductDTO();
			dto.Active = true;
			dto.Title = value.Title;
			dto.Description = value.Description;
			dto.MinimalAmount = int.Parse(value.MinimalAmount);
			dto.StartsAt = decimal.Parse(value.StartsAt.Replace("R", "").Replace("$", "").Replace(",", ".").Replace(" ", ""));
			dto.Tags = value.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			await _productServices.CreateProduct(dto);
		}
	}
}
