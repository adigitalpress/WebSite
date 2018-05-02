using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace aDigital.ProductsAndServices.Infra
{
	public class ProductRepository : IProductRepository
	{
		string _storageConnectionString;
		public CloudTable _table;
		ILogger _logger;
		public ProductRepository(string storageConnectionString, ILogger logger)
		{
			_storageConnectionString = storageConnectionString;
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
			var tableClient = storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("products");
			_logger = logger;
		}

		public async Task Create(IProduct product)
		{
			var filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "aDigital");
			TableContinuationToken token = null;
			IEnumerable<int> rowKeys = Enumerable.Empty<int>();
			var query = new TableQuery<ProductTableEntity>().Select(new string[] { "RowKey" }).Where(filter);

			do
			{
				var result = await _table.ExecuteQuerySegmentedAsync(query, token);
				rowKeys = rowKeys.Union(result.Results.Select(i => i.Id));
			} while (token != null);

			ProductTableEntity p = new ProductTableEntity();
			p.Description = product.Description;
			p.Id = (rowKeys?.Max() ?? 0) + 1;
			p.Images = product.Images;
			p.MinimalAmount = product.MinimalAmount;
			p.PartitionKey = "aDigital";
			p.PresetAmounts = product.PresetAmounts;
			p.StartsAt = product.StartsAt;
			p.Tags = product.Tags;
			p.Title = product.Title;
			p.UnitName = product.UnitName;
			p.Active = true;

			var op = TableOperation.Insert(p);

			await _table.ExecuteAsync(op);

			product.Id = p.Id;
		}

		public async Task<IEnumerable<IProduct>> GetProductsById(IEnumerable<int> productIds)
		{
			if (productIds == null || !productIds.Any())
			{
				return null;
			}
			var products = new List<ProductTableEntity>();
			if (productIds.Count() == 1)
			{
				var op = TableOperation.Retrieve<ProductTableEntity>("aDigital", productIds.First().ToString());
				var query = await _table.ExecuteAsync(op);
				products.Add((ProductTableEntity)query.Result);
			}
			else
			{
				var orClause = "";
				foreach (var item in productIds)
				{
					var filter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, item.ToString());
					orClause = TableQuery.CombineFilters(orClause, TableOperators.Or, filter);
				}
				var gFilter = TableQuery.CombineFilters(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "aDigital"), TableOperators.And, orClause);
				var query = new TableQuery<ProductTableEntity>().Where(gFilter);
				TableContinuationToken token = null;

				try
				{
					do
					{
						var result = await _table.ExecuteQuerySegmentedAsync(query, token);
						products.AddRange(result.Results);
						token = result.ContinuationToken;
					} while (token != null);
				}
				catch (Exception ex)
				{
					ExceptionTelemetry t = new ExceptionTelemetry();
					t.Exception = ex;
					_logger.LogException(t);
					throw;
				}
			}
			return products;
		}

		public async Task<IEnumerable<IProduct>> GetProducts()
		{
			var filter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "aDigital");
			TableContinuationToken token = null;
			var products = new List<IProduct>();
			var query = new TableQuery<ProductTableEntity>().Where(filter);

			do
			{
				var result = await _table.ExecuteQuerySegmentedAsync(query, token);
				products.AddRange(result.Results);
			} while (token != null);

			return products;
		}
	}
}
