using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aDigital.Library;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using aDigital.Blog.Infra.SpecificDTOs;

namespace aDigital.Blog.Infra
{
	public class BlogRepository : IBlogRepository
	{
		string _storageConnectionString;
		public CloudTable _table;
		public BlogRepository(string storageConnectionString)
		{
			_storageConnectionString = storageConnectionString;
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
			var tableClient = storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("blogPosts");
		}
		public BlogRepository()
		{
			//constructor for testing propurses
		}

		public string Config()
		{
			return _storageConnectionString ?? "NULL";
		}

		public async Task<IEnumerable<IBlogEntry>> List()
		{
			var query = new TableQuery<BlogEntryTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "aDigital"));
			TableContinuationToken token = null;
			List<BlogEntryTableEntity> posts = new List<BlogEntryTableEntity>();

			do
			{
				var result = await _table.ExecuteQuerySegmentedAsync(query, token);
				posts.AddRange(result.Results);
				token = result.ContinuationToken;
			} while (token != null);

			return posts;
		}

		public async Task<IBlogEntry> List(string id)
		{
			int rowKey = 0;
			if (int.TryParse(id, out rowKey))
			{
				var entity = await _table.ExecuteAsync(TableOperation.Retrieve<BlogEntryTableEntity>("aDigital", id));
				return (IBlogEntry)entity.Result;
			}
			var query = new TableQuery<BlogEntryTableEntity>()
								.Where(TableQuery.CombineFilters(
										TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "aDigital"),
										TableOperators.And,
										TableQuery.CombineFilters(
											TableQuery.GenerateFilterCondition("Title", QueryComparisons.Equal, id),
											TableOperators.Or,
											TableQuery.GenerateFilterCondition("URLTitle", QueryComparisons.Equal, id.ToLower()))));

			TableContinuationToken token = null;
			List<BlogEntryTableEntity> posts = new List<BlogEntryTableEntity>();

			do
			{
				var result = await _table.ExecuteQuerySegmentedAsync(query, token);
				posts.AddRange(result.Results);
				token = result.ContinuationToken;
			} while (token != null);

			return posts.FirstOrDefault();
		}

		public Task<bool> Save(BlogEntryDTO blogEntry)
		{
			throw new NotImplementedException();
		}
	}
}
