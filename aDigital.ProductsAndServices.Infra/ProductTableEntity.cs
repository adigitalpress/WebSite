using System;
using System.Collections.Generic;
using System.Linq;
using aDigital.Library;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace aDigital.ProductsAndServices.Infra
{
	public class ProductTableEntity : TableEntity, IProduct
	{
		public ProductTableEntity()
		{
			PartitionKey = "aDigital";
			PresetAmounts = Enumerable.Empty<int>();
			Images = Enumerable.Empty<string>();
			Tags = Enumerable.Empty<string>();
		}
		[IgnoreProperty]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public double StartsAt { get; set; }
		public int MinimalAmount { get; set; }
		public bool Active { get; set; }
		public string UnitName { get; set; }

		[IgnoreProperty]
		public IEnumerable<int> PresetAmounts
		{ get; set; }
		[IgnoreProperty]
		public IEnumerable<string> Images { get; set; }
		[IgnoreProperty]
		public IEnumerable<string> Tags { get; set; }

		public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
		{
			base.ReadEntity(properties, operationContext);
			EntityProperty propertie = null;
			PresetAmounts = properties.TryGetValue(nameof(PresetAmounts), out propertie) ? JsonConvert.DeserializeObject<IEnumerable<int>>(propertie.StringValue) : PresetAmounts;
			Images = properties.TryGetValue(nameof(Images), out propertie) ? JsonConvert.DeserializeObject<IEnumerable<string>>(propertie.StringValue) : Images;
			Id = int.Parse(RowKey);
		}

		public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
		{
			RowKey = Id.ToString();
			var op = base.WriteEntity(operationContext);
			op.Add(nameof(PresetAmounts), new EntityProperty(JsonConvert.SerializeObject(PresetAmounts)));
			op.Add(nameof(Images), new EntityProperty(JsonConvert.SerializeObject(Images)));
			return op;
		}
	}
}
