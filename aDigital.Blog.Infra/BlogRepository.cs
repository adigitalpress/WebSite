using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aDigital.Library;
using System.Linq;

namespace aDigital.Blog.Infra
{
	public class BlogRepository : IBlogRepository
	{
		public Task<IEnumerable<BlogEntryDTO>> List()
		{
			var aux = new BlogEntryInfra();
			aux.Body = "bla";
			IEnumerable<BlogEntryDTO> list = new List<BlogEntryDTO> { aux };
			return Task.FromResult(list);
		}

		public Task<bool> Save(BlogEntryDTO blogEntry)
		{
			throw new NotImplementedException();
		}
	}
}
