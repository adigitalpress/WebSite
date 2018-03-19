using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using aDigital.Blog.Infra.SpecificDTOs;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using Xunit;

namespace aDigital.Blog.Infra.Tests
{
	public class BlogRepositoryTests
	{
		[Fact]
		public async Task BasicListTest01()
		{
			var rep = new BlogRepository();

			var cloudTableMock = new Mock<CloudTable>(MockBehavior.Loose, new Uri("http://a.a"));
			var queryResult = new BlogEntryTableEntity();
			TableResult r = new TableResult();
			r.Result = queryResult;

			cloudTableMock.Setup(i => i.ExecuteAsync(It.IsAny<TableOperation>())).ReturnsAsync(r);
			rep._table = cloudTableMock.Object;

			var op = await rep.List("10");
			Assert.NotNull(op);
			cloudTableMock.Verify(i => i.ExecuteAsync(It.IsAny<TableOperation>()), Times.Once());
		}

		[Fact]
		public async Task BasicListTest02()
		{
			var rep = new BlogRepository();

			var cloudTableMock = new Mock<CloudTable>(MockBehavior.Loose, new Uri("http://a.a"));
			var queryResult = new List<BlogEntryTableEntity> { new BlogEntryTableEntity() };
			var r = ObtainInstance();

			var setup = cloudTableMock.Setup(i => i.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<BlogEntryTableEntity>>(), It.IsAny<TableContinuationToken>()));
			setup.ReturnsAsync(r);
			rep._table = cloudTableMock.Object;

			var op = await rep.List("myBlog");
			Assert.NotNull(op);
			cloudTableMock.Verify(i => i.ExecuteAsync(It.IsAny<TableOperation>()), Times.Never());
			cloudTableMock.Verify(i => i.ExecuteQuerySegmentedAsync(It.IsAny<TableQuery<BlogEntryTableEntity>>(), It.IsAny<TableContinuationToken>()), Times.Once());
		}

		private TableQuerySegment<BlogEntryTableEntity> ObtainInstance()
		{
			var ctor = typeof(TableQuerySegment<BlogEntryTableEntity>)
				.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
				.FirstOrDefault(c => c.GetParameters().Count() == 1);

			var mockQuerySegment = ctor.Invoke(new object[] { new List<BlogEntryTableEntity>() { new BlogEntryTableEntity() { Title = "myBlog" } } }) as TableQuerySegment<BlogEntryTableEntity>;

			return mockQuerySegment;
		}
	}
}
