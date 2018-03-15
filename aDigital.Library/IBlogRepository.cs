using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface IBlogRepository
	{
		Task<IEnumerable<IBlogEntry>> List();
		Task<IBlogEntry> List(string id);
		Task<bool> Save(BlogEntryDTO blogEntry);
		string Config();
	}
}
