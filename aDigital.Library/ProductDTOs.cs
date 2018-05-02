using System;
using System.Collections.Generic;
using aDigital.Library;

namespace aDigital.Library.ProductsAndServices
{
	public class ProductDTO : ProductQueryDTO, IProduct
	{
		public ProductDTO()
		{
		}
		public IEnumerable<int> PresetAmounts { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public bool Active { get; set; }
	}

	public class ProductQueryDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double StartsAt { get; set; }
		public string Description { get; set; }
		public IEnumerable<string> Images { get; set; }
		public string UnitName { get; set; }
		public int MinimalAmount { get; set; }
	}

	public class ProductCreationDTO
	{
		public string MinimalAmount { get; set; }
		public string Title { get; set; }
		public string StartsAt { get; set; }
		public string Description { get; set; }
		public string Tags { get; set; }
		public string UnitName { get; set; }
	}
}
