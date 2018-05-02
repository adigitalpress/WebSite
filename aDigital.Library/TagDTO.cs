using System;
using System.Collections.Generic;

namespace aDigital.Library
{
	public interface ITag
	{
		string Title { get; set; }
		IEnumerable<string> Synonyms { get; set; }
		IEnumerable<TagAssociationContext> Associations { get; set; }
		void AssociateTo(string objectId, int contextId);
	}

	public class TagDTO : ITag
	{
		public TagDTO()
		{
		}
		public string Title { get; set; }
		public IEnumerable<string> Synonyms { get; set; }
		public IEnumerable<TagAssociationContext> Associations { get; set; }

		public void AssociateTo(string objectId, int contextId)
		{
			throw new NotImplementedException();
		}
	}

	public class TagAssociationContext
	{
		public int ContextId { get; set; }
		public string ObjectId { get; set; }
	}
}
