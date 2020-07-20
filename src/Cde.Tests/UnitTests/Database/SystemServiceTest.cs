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
	public class SystemServiceTest
	{
		[Fact]
		public void GetAllUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<SystemModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new SystemService(dbContext);
				var expected = fakeContext.GetFakeData<SystemModel>();
				var result = service.GetAll().ToList();

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData(1)]
		public void GetByIdUserTest(int id) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<SystemModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new SystemService(dbContext);
				var expected = fakeContext.GetFakeData<SystemModel>().FirstOrDefault(x => x.Id == id);
				var result = service.Get(x => x.Id == id).FirstOrDefault();

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData("test")]
		public void CreateUserTest(string name) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<SystemModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new SystemService(dbContext);
				var fakeUser = new SystemModel() { Name = name };
				service.Create(fakeUser);
				var result = dbContext.Set<SystemModel>().Where(x => x.Name == name).First();

				result.Should().NotBeNull();
			}
		}

		[Fact]
		public void UpdateUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<SystemModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var date = DateTime.UtcNow;
				var newName = "update level";
				var service = new SystemService(dbContext);
				var user = service.Get(x => x.Id == 1).First();
				user.Name = newName;
				service.Update(user);
				var result = dbContext.Set<SystemModel>().Where(l => l.Id == user.Id).FirstOrDefault();

				result.Name.Should().Be(newName);
			}
		}

		[Fact]
		public void DeleteUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<SystemModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new SystemService(dbContext);
				var user = service.Get(x => x.Id == 1).First();
				service.Delete(user);
				var result = dbContext.Set<SystemModel>().Where(l => l.Id == user.Id).FirstOrDefault();

				result.Should().BeNull();
			}
		}
	}
}
