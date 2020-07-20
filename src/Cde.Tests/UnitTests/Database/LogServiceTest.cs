using Cde.Database;
using Cde.Database.Services;
using Cde.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cde.Tests.UnitTests.Database
{
	public class LogServiceTest
	{
		[Fact]
		public void GetAllLogsTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LogService(dbContext);
				var expected = fakeContext.GetFakeData<LogModel>();
				var result = service.GetAll().ToList();

				result.Should().BeEquivalentTo(expected);

			}
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public void GetAllBySystemLogsTest(int systemId) {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LogService(dbContext);
				var result = service.GetPageBySystemId(systemId, 10);
				var expected = fakeContext.GetFakeData<LogModel>()
					.Where(x => x.SystemId == systemId);
				
				result.Should().HaveCount(expected.Count());
			}
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public void GetByIdLogsTest(int id) {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LogService(dbContext);
				var expected = fakeContext.GetFakeData<LogModel>().First(x => x.Id == id);
				var result = service.GetById(id);

				result.Id.Should().Equals(expected.Id);
			}
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(1, 3)]
		public void GetByLevelLogsTest(int systemId, int levelId) {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LogService(dbContext);
				var expected = fakeContext.GetFakeData<LogModel>().Where(x => x.SystemId == systemId && x.LevelId == levelId);
				var result = service.GetPageBySystemAndLevel(systemId, levelId, 10);

				result.Should().HaveCount(expected.Count());
			}
		}

		[Theory]
		[InlineData(1, 3)]
		public void GetRecentByLevelLogTest(int systemId, int levelId) {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LogService(dbContext);
				var expected = fakeContext.GetFakeData<LogModel>().Where(x => x.SystemId == systemId && x.LevelId == levelId).OrderByDescending(x => x.CreatedAt).First();
				var result = service.GetRecentByLevel(systemId, levelId);

				result.Id.Should().Be(expected.Id);
			}
		}

		[Theory]
		[InlineData("Giro Pops", "Giro Pops details", 2, 2)]
		public void CreateLogTest(string title, string details, int systemId, int levelId) {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var fakeLog = new LogModel() { Title = title, Details = details, CreatedAt = DateTime.UtcNow, SystemId = systemId, LevelId = levelId };
				var service = new LogService(dbContext);
				service.Create(fakeLog);
				var result = dbContext.Set<LogModel>().FirstOrDefault(l => l.Title == title);

				result.Should().NotBeNull();
			}
		}

		[Fact]
		public void UpdateLogTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var date = DateTime.UtcNow;
				var service = new LogService(dbContext);
				var log = service.GetById(1);
				log.CreatedAt = date;
				service.Update(log);
				var result = dbContext.Set<LogModel>().FirstOrDefault(l => l.Id == log.Id);

				result.CreatedAt.Should().Be(date);
			}
		}

		[Fact]
		public void DeleteLogTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWithAll();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LogService(dbContext);
				var log = service.Get(x => x.Id == 1).First();
				service.Delete(log);
				var result = dbContext.Set<LogModel>().FirstOrDefault(l => l.Id == log.Id);

				result.Should().BeNull();
			}
		}
	}
}
