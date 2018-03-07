using System;
using System.Collections.Generic;
using aDigital.Library;

namespace aDigital.Blog.Domain
{
	public class BlogEntry : BlogEntryDTO
	{
		IBlogServices _blogService;
		public BlogEntry(IBlogServices service)
		{
			_blogService = service;
		}

		public override void Publish()
		{
			throw new NotImplementedException();
		}
	}
}
