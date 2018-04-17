using System;
using System.Collections.Generic;
using aDigital.Library;

namespace aDigital.ProductsAndServices
{
	public class ProductDTO : IProduct
	{
		public ProductDTO()
		{
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal StartsAt { get; set; }
		public int MinimalAmount { get; set; }
		public IEnumerable<int> PresetAmounts { get; set; }
		public IEnumerable<string> Images { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public bool Active { get; set; }
	}
}
