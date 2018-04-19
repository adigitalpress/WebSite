using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface IProductRepository
	{
		Task Create(IProduct product);
		Task<IEnumerable<IProduct>> GetProductsById(IEnumerable<int> productIds);
		Task<IEnumerable<IProduct>> GetProducts();
	}
}
