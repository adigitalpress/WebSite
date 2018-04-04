using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace aDigital.Tags.Infra
{
	public class TagsRepository : ITagRepository
	{
		string _storageConnectionString;
		public CloudTable _table;
		ILogger _logger;
		public TagsRepository(string storageConnectionString, ILogger logger)
		{
			_storageConnectionString = storageConnectionString;
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
			var tableClient = storageAccount.CreateCloudTableClient();
			_table = tableClient.GetTableReference("tagsRepository");
			_logger = logger;
		}

		public async Task<IEnumerable<TagAssociationContext>> GetAssociatedObjects(string title, int? context = null)
		{

			var query = new TableQuery<TagTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "aDigital"));

			TableContinuationToken token = null;
			List<TagTableEntity> tags = new List<TagTableEntity>();
			try
			{
				do
				{
					var result = await _table.ExecuteQuerySegmentedAsync(query, token);
					tags.AddRange(result.Results);
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
			var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			var options = CompareOptions.IgnoreCase |
				CompareOptions.IgnoreSymbols |
				CompareOptions.IgnoreNonSpace;
			var ids = tags
						.Where(t => compareInfo.IndexOf(t.Title, title, options) > -1 ||
									   compareInfo.IndexOf(t._synonymString ?? "", title, options) > -1)
						.SelectMany(t => t.Associations)
						.Where(a => a.ContextId == (context ?? a.ContextId));

			return ids;

		}

		public async Task CreateTag(ITag tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag));
			}
			var mappedObject = new TagTableEntity();
			mappedObject.Title = tag.Title;
			mappedObject.Associations = tag.Associations;
			mappedObject.Synonyms = tag.Synonyms;
			var op = TableOperation.Insert(mappedObject);
			try
			{
				await _table.ExecuteAsync(op);
			}
			catch (Exception ex)
			{
				var e = new ExceptionTelemetry();
				e.Exception = ex;
				e.Properties.Add("tagObject", JsonConvert.SerializeObject(tag));
				_logger.LogException(e);
			}
		}

		public async Task<ITag> GetTag(string title)
		{
			if (title == null)
			{
				return null;
			}
			TableOperation op = TableOperation.Retrieve<TagTableEntity>("aDigital", title);
			try
			{
				var result = await _table.ExecuteAsync(op);
				return (ITag)result.Result;
			}
			catch (Exception ex)
			{
				var t = new ExceptionTelemetry();
				t.Exception = ex;
				t.Properties.Add("title", title);
				_logger.LogException(t);
				throw;
			}
		}

		public async Task SaveTag(ITag tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag));
			}
			var mappedObject = new TagTableEntity();
			mappedObject.PartitionKey = "aDigital";
			mappedObject.Title = tag.Title;
			mappedObject.Associations = tag.Associations;
			mappedObject.Synonyms = tag.Synonyms;
			mappedObject.ETag = "*";
			try
			{
				var op = TableOperation.Replace(mappedObject);

				await _table.ExecuteAsync(op);
			}
			catch (Exception ex)
			{
				var e = new ExceptionTelemetry();
				e.Exception = ex;
				e.Properties.Add("tagObject", JsonConvert.SerializeObject(tag));
				_logger.LogException(e);
			}
		}
	}
}