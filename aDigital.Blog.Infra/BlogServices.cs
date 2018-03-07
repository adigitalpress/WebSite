using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Library;

namespace aDigital.Blog.Infra
{
	public class BlogServices : IBlogServices
	{
		IBlogRepository _repo;
		public BlogServices(IBlogRepository repo)
		{
			_repo = repo;
		}

		public async Task<IEnumerable<IBlogEntry>> List(int pageSize, int pageNumber)
		{
			return await _repo.List();
		}

		public async Task<IEnumerable<IBlogEntry>> ListTop5()
		{
			throw new NotImplementedException();
		}
	}
}
