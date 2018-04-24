using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface ITagsService
	{
		Task CreateTag(string tagTitle);
		Task<IEnumerable<TagAssociationContext>> SearchTagAssociationsAsync(string search);
		Task Associate(string tagTitle, string objectId, int contextId);
		Task<IEnumerable<string>> GetTags();
	}
}
