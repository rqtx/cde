using Cde.Database;
using Cde.Database.Services;
using Cde.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Cde.Tests.UnitTests.Database
{
	public class LevelServiceUnitTest
	{
		[Fact]
		public void GetAllUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<LevelModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LevelService(dbContext);
				var expected = fakeContext.GetFakeData<LevelModel>();
				var result = service.GetAll().ToList();

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData(1)]
		public void GetByIdUserTest(int id) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<LevelModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LevelService(dbContext);
				var expected = fakeContext.GetFakeData<LevelModel>().FirstOrDefault(x => x.Id == id);
				var result = service.Get(x => x.Id == id).FirstOrDefault();

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData("test")]
		public void CreateUserTest(string level) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<LevelModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LevelService(dbContext);
				var fakeUser = new LevelModel() { Name = level };
				service.Create(fakeUser);
				var result = dbContext.Set<LevelModel>().Where(x => x.Name == level).First();

				result.Should().NotBeNull();
			}
		}

		[Fact]
		public void UpdateUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<LevelModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var date = DateTime.UtcNow;
				var newName = "update level";
				var service = new LevelService(dbContext);
				var level = service.Get(x => x.Id == 1).First();
				level.Name = newName;
				service.Update(level);
				var result = dbContext.Set<LevelModel>().Where(l => l.Id == level.Id).FirstOrDefault();

				result.Name.Should().Be(newName);
			}
		}

		[Fact]
		public void DeleteUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<LevelModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new LevelService(dbContext);
				var user = service.Get(x => x.Id == 1).First();
				service.Delete(user);
				var result = dbContext.Set<LevelModel>().Where(l => l.Id == user.Id).FirstOrDefault();

				result.Should().BeNull();
			}
		}
	}
}
