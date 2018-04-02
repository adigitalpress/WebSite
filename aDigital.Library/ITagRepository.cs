using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface ITagRepository
	{
		Task<IEnumerable<TagAssociationContext>> GetAssociatedObjects(string title, int? context = null);
		Task CreateTag(ITag tag);
		Task<ITag> GetTag(string title);
		Task SaveTag(ITag tag);
	}
}
