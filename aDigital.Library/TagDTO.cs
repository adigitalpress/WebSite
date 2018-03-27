using System;
using System.Collections.Generic;

namespace aDigital.Library
{
	public class TagDTO
	{
		public TagDTO()
		{
		}
		public Guid Id { get; set; }
		public string Title { get; set; }
		public IEnumerable<string> Synonyms { get; set; }
		public IEnumerable<TagAssociationContext> Associations { get; set; }
	}

	public class TagAssociationContext
	{
		public int ContextId { get; set; }
		public object ObjectId { get; set; }
	}
}
