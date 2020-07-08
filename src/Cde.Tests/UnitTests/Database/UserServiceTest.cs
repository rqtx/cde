using Cde.Database;
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
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new DatabaseService<UserModel>(dbContext);
				var expected = fakeContext.GetFakeData<UserModel>();
				var result = service.GetAll().ToList();

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData(1)]
		public void GetByIdUserTest(int id) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new DatabaseService<UserModel>(dbContext);
				var expected = fakeContext.GetFakeData<UserModel>().FirstOrDefault(x => x.Id == id);
				var result = service.Get(x => x.Id == id).FirstOrDefault();

				result.Should().BeEquivalentTo(expected);
			}
		}

		[Theory]
		[InlineData("Giro Pops", "giropops@email.com", "test", "test")]
		public void CreateUserTest(string name, string email, string salt, string passhash) {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new DatabaseService<UserModel>(dbContext);
				var fakeUser = new UserModel() { Name = name, Email = email, Salt = salt, Passhash = passhash, CreatedAt = DateTime.UtcNow };
				service.Create(fakeUser);
				var result = dbContext.Set<UserModel>().Where(x => x.Email == email).First();

				result.Should().NotBeNull();
			}
		}

		[Fact]
		public void UpdateUserTest() {
			var fakeContext = new FakeContext();
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var date = DateTime.UtcNow;
				var newName = "update name";
				var service = new DatabaseService<UserModel>(dbContext);
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
			fakeContext.FillWith<UserModel>();

			using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
				var service = new DatabaseService<UserModel>(dbContext);
				var user = service.Get(x => x.Id == 1).First();
				service.Delete(user);
				var result = dbContext.Set<UserModel>().Where(l => l.Id == user.Id).FirstOrDefault();

				result.Should().BeNull();
			}
		}
	}
}
