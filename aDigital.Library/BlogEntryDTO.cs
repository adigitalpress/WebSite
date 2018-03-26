using System;
using System.Collections.Generic;

namespace aDigital.Library
{
	public interface IBlogEntry
	{
		string Title { get; }
		string URLTitle { get; }
		string Body { get; }
		IEnumerable<string> Tags { get; }
		string PublishedBy { get; }
		DateTime PublishedOn { get; }
		string HeadImage { get; }
		int SequentialId { get; }
		string Description { get; }
		Guid UUID { get; }
		void Publish();
	}

	public class BlogEntryDTO : IBlogEntry
	{
		public virtual string Title { get; set; }
		public virtual string Body { get; set; }
		public virtual IEnumerable<string> Tags { get; set; }
		public virtual string PublishedBy { get; set; }
		public virtual DateTime PublishedOn { get; set; }
		public virtual string HeadImage { get; set; }
		public virtual string URLTitle { get; set; }
		public virtual string Description { get; set; }
		public virtual int SequentialId { get; set; }
		public virtual Guid UUID { get; set; }
		public virtual void Publish() { throw new NotImplementedException(); }
	}
}
