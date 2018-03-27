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
		ILogger _logger;
		public BlogRepository(string storageConnectionString, ILogger logger)
		{
			_storageConnectionString = storageConnectionString;
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
			var tableClient = storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("blogPosts");
			_logger = logger;
		}
		public BlogRepository(ILogger logger = null)
		{
			//constructor for testing propurses
			_logger = logger;
		}

		public string Config()
		{
			return _storageConnectionString ?? "NULL";
		}

		public async Task<IEnumerable<IBlogEntry>> List()
		{
			try
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
			catch (Exception ex)
			{
				ExceptionTelemetry t = new ExceptionTelemetry();
				t.Exception = ex;
				_logger.LogException(t);
				throw;
			}
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
