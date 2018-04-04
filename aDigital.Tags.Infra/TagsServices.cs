using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;

namespace aDigital.Tags.Infra
{
	public class TagsServices : ITagsService
	{
		ITagRepository tagRepository;
		IServiceProvider container;
		public TagsServices(ITagRepository tagRepository, IServiceProvider container)
		{
			this.tagRepository = tagRepository;
			this.container = container;
		}

		public async Task CreateTag(string tagTitle)
		{
			//Check cache for existing tag in the future
			var tag = (ITag)container.GetService(typeof(ITag));
			tag.Title = tagTitle;
			await tagRepository.CreateTag(tag);
		}

		public async Task Associate(string tagTitle, string objectId, int contextId)
		{
			var tag = await tagRepository.GetTag(tagTitle);
			if (tag.Associations == null || !tag.Associations.Select(i => i.ObjectId).Contains(objectId))
			{
				tag.AssociateTo(objectId, contextId);
			}
			await tagRepository.SaveTag(tag);
		}

		public async Task<IEnumerable<TagAssociationContext>> SearchTagAssociationsAsync(string search)
		{
			var itemsToSearchFor = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			var tasks = new List<Task>();
			var result = Enumerable.Empty<TagAssociationContext>();
			foreach (var item in itemsToSearchFor)
			{
				tasks.Add(tagRepository.GetAssociatedObjects(item).ContinueWith((prev) =>
				{
					result = result.Union(prev.Result);
				}));
			}
			await Task.WhenAll(tasks);
			result = result
						.GroupBy(i => new TagAssociationContext()
						{
							ObjectId = i.ObjectId,
							ContextId = i.ContextId
						})
						.OrderBy(i => i.Count())
						.Select(i => i.Key);
			return result;
		}
	}
}
