using Cde.Controllers;
using Cde.Database;
using Cde.Models;
using Cde.Models.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Cde.Tests.UnitTests.Controllers
{
	public class UserControllerTest
	{
        [Fact]
        public void Should_Be_Ok_When_GetAll() {
            var fakeContext = new FakeContext();
            fakeContext.FillWith<UserModel>();
            fakeContext.FillWith<RoleModel>();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new DatabaseService<UserModel>(dbContext);
                var controller = new UserController(dbContext, fakeContext.Mapper);
                var result = controller.GetAllUsers();
                var expected = fakeContext.Mapper.Map <List<UserDTO>>(service.GetAll().ToList());

                Assert.IsType<OkObjectResult>(result.Result);
                result = (result.Result as OkObjectResult).Value as List<UserDTO>;
                result.Value.Should().BeEquivalentTo(expected);
            }
        }

        [Theory]
        [InlineData(1)]
        public void Should_Be_Ok_When_Get_By_Id(int id) {
            var fakeContext = new FakeContext();
            fakeContext.FillWith<UserModel>();
            fakeContext.FillWith<RoleModel>();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new DatabaseService<UserModel>(dbContext);
                var controller = new UserController(dbContext, fakeContext.Mapper);
                var result = controller.GetUser(id);
                var expected = fakeContext.Mapper.Map<UserDTO>(service.Get(s => s.Id == id).FirstOrDefault());
                
                Assert.IsType<OkObjectResult>(result.Result);
                result = (result.Result as OkObjectResult).Value as UserDTO;
                result.Value.Should().BeEquivalentTo(expected);
            }
        }

        [Theory]
        [InlineData(60)]
        public void Should_Be_Not_Found_When_Get_By_Id(int id) {
            var fakeContext = new FakeContext();
            fakeContext.FillWith<UserModel>();
            fakeContext.FillWith<RoleModel>();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new DatabaseService<UserModel>(dbContext);
                var controller = new UserController(dbContext, fakeContext.Mapper);
                var result = controller.GetUser(id);

                Assert.IsType<NotFoundObjectResult>(result.Result);
                result.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Should_Be_OK_When_Post() {
            var fakeContext = new FakeContext();
            fakeContext.FillWith<UserModel>();
            fakeContext.FillWith<RoleModel>();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new DatabaseService<UserModel>(dbContext);
                var controller = new UserController(dbContext, fakeContext.Mapper);
                var form = new UserFormDTO() {
                    Role = "user",
                    Name = "testxtg",
                    Password = "123456"
                };
                var result = controller.Post(form);
                var  expected= service.Get(s => s.Name == form.Name).FirstOrDefault();
                Assert.IsType<CreatedResult>(result.Result);
                result = (result.Result as CreatedResult).Value as UserModel;
                result.Value.Should().NotBeNull();
                result.Value.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public void Should_Be_OK_When_Delete() {
            var fakeContext = new FakeContext();
            fakeContext.FillWith<UserModel>();
            fakeContext.FillWith<RoleModel>();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new DatabaseService<UserModel>(dbContext);
                var controller = new UserController(dbContext, fakeContext.Mapper);
       
                controller.Delete(1);
                var result = service.Get(s => s.Id == 1).FirstOrDefault();
                result.Should().BeNull();
            }
        }
    }
}
