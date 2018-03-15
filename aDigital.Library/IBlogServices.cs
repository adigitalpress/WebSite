using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aDigital.Library
{
	public interface IBlogServices
	{
		Task<IEnumerable<IBlogEntry>> ListTop5();
		Task<IEnumerable<IBlogEntry>> List(int pageSize, int pageNumber);
		Task<IBlogEntry> List(string id);
		string Config();
	}
}
