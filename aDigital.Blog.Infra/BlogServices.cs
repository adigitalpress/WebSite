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

		public string Config()
		{
			return _repo.Config();
		}

		public async Task<IEnumerable<IBlogEntry>> List(int pageSize, int pageNumber)
		{
			var posts = await _repo.List();
			return posts.OrderByDescending(a => a.PublishedOn);
		}

		public async Task<IBlogEntry> List(string id)
		{
			var posts = await _repo.List(id);
			return posts;
		}

		public async Task<IEnumerable<IBlogEntry>> ListTop5()
		{
			throw new NotImplementedException();
		}
	}
}
