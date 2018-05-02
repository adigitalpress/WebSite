using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface IProductServices
	{
		Task<IEnumerable<IProduct>> GetProducts();
		Task<IEnumerable<IProduct>> GetProductById(int id);
		Task<IEnumerable<IProduct>> SearchProduct(string query);

		Task CreateProduct(IProduct product);
	}
}
