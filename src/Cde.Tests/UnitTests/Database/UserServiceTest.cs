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
	public class UserServiceTest
	{
		[Fact]
		public void GetAllUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<RoleModel>();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new UserService(dbContext);
				var expected = fakeContext.GetFakeData<UserModel>();
				var result = service.GetAll().ToList();

				result.Should().HaveCount(expected.Count());
			}
		}

		[Theory]
		[InlineData(1)]
		public void GetByIdUserTest(int id) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<RoleModel>();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new UserService(dbContext);
				var expected = fakeContext.GetFakeData<UserModel>().FirstOrDefault(x => x.Id == id);
				expected.Role = fakeContext.GetFakeData<RoleModel>().FirstOrDefault(x => x.Id == expected.RoleId);
				expected.Role.Users = null;
				var result = service.Get(x => x.Id == id).FirstOrDefault();
				result.Role.Users = null;

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData("Giro Pops", 1, "test", "test")]
		public void CreateUserTest(string name, int role, string salt, string passhash) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<UserModel>();
			fakeContext.FillWith<RoleModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new UserService(dbContext);
				var fakeUser = new UserModel() { Name = name, RoleId = role, Salt = salt, Passhash = passhash, CreatedAt = DateTime.UtcNow };
				service.Create(fakeUser);
				var result = dbContext.Set<UserModel>().Where(x => x.Name == name).First();

				result.Should().NotBeNull();
			}
		}

		[Fact]
		public void UpdateUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<RoleModel>();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var date = DateTime.UtcNow;
				var newName = "update name";
				var service = new UserService(dbContext);
				var user = service.Get(x => x.Id == 1).First();
				user.Name = newName;
				service.Update(user);
				var result = dbContext.Set<UserModel>().Where(l => l.Id == user.Id).FirstOrDefault();

				result.Name.Should().Be(newName);
			}
		}

		[Fact]
		public void DeleteUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<RoleModel>();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new UserService(dbContext);
				var user = service.Get(x => x.Id == 1).First();
				service.Delete(user);
				var result = dbContext.Set<UserModel>().Where(l => l.Id == user.Id).FirstOrDefault();

				result.Should().BeNull();
			}
		}
	}
}
