using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace aDigital.ProductsAndServices.Infra
{
	public class ProductServices : IProductServices
	{
		IProductRepository productRepository;
		string _tagServicesUrl;
		public ProductServices(IProductRepository repository, IConfiguration configuration)
		{
			productRepository = repository;
			_tagServicesUrl = configuration["tagServicesBaseUrl"];
		}

		public async Task<IEnumerable<IProduct>> SearchProduct(string query)
		{
			var cli = new RestClient(_tagServicesUrl);
			var request = new RestRequest("/associations/" + System.Net.WebUtility.UrlEncode(query));
			request.Method = Method.GET;

			var result = await cli.ExecuteTaskAsync(request);
			var tags = JsonConvert.DeserializeObject<IEnumerable<TagAssociationContext>>(result.Content);
			tags = tags.Where(t => t.ContextId == 0);
			var productsIds = tags.Select((arg) => int.Parse(arg.ObjectId));
			var products = await productRepository.GetProductsById(productsIds.Distinct());
			var merge = from p in products
						join i in productsIds on p.Id equals i
						select p;
			return merge;
		}

		public async Task CreateProduct(IProduct product)
		{
			await productRepository.Create(product);
			List<Task> tasks = new List<Task>();
			RestClient client = new RestClient(_tagServicesUrl);
			foreach (var item in product.Tags)
			{
				var request = new RestRequest("/associations/");
				request.Method = Method.POST;
				request.AddJsonBody(new { Id = item, AssociatedObjectId = product.Id.ToString() });

				tasks.Add(client.ExecuteTaskAsync(request));
			}
			await Task.WhenAll(tasks);
		}
	}
}
