using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface IBlogRepository
	{
		Task<IEnumerable<BlogEntryDTO>> List();
		Task<bool> Save(BlogEntryDTO blogEntry);
	}
}
