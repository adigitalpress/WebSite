using System;
using System.Collections.Generic;
using aDigital.Library;
using Microsoft.WindowsAzure.Storage.Table;

namespace aDigital.Blog.Infra.SpecificDTOs
{
	public class BlogEntryTableEntity : TableEntity, IBlogEntry
	{
		public BlogEntryTableEntity()
		{
			this.PartitionKey = "aDigital";
		}

		public string Title { get; set; }

		public string Body { get; set; }

		public IEnumerable<string> Tags { get; set; }

		public string PublishedBy { get; set; }

		public DateTime PublishedOn { get; set; }

		public string HeadImage { get; set; }

		public string URLTitle { get; set; }

		public void Publish()
		{
			throw new NotImplementedException();
		}

		public int SequentialId { get { return int.Parse(this.RowKey); } }
	}
}
