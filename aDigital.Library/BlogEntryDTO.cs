using System;
using System.Collections.Generic;

namespace aDigital.Library
{
	public interface IBlogEntry
	{
		string Title { get; }
		string Body { get; }
		IEnumerable<string> Tags { get; }
		string PublishedBy { get; }
		DateTime PublishedOn { get; }

		void Publish();
	}

	public abstract class BlogEntryDTO : IBlogEntry
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public string PublishedBy { get; set; }
		public DateTime PublishedOn { get; set; }

		public abstract void Publish();
	}
}
