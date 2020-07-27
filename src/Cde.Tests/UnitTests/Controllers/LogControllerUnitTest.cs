using Cde.Controllers;
using Cde.Database;
using Cde.Database.Services;
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
	public class LogControllerUnitTest
	{
        [Theory]
        [InlineData(1)]
        public void Should_Be_Ok_When_GetAll(int systemId) {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);
                var result = controller.GetAll(systemId);

                Assert.IsType<OkObjectResult>(result.Result);
            }
        }

        [Theory]
        [InlineData(1, 1)]
        public void Should_Be_Ok_When_GetByLevel(int systemId, int levelId) {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);
                var result = controller.GetByLevel(systemId, levelId);

                Assert.IsType<OkObjectResult>(result.Result);
            }
        }

        [Theory]
        [InlineData(1)]
        public void Should_Be_Ok_When_GetOverview(int systemId) {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);
                var result = controller.GetSystemOverview(systemId);

                Assert.IsType<OkObjectResult>(result.Result);
            }
        }

        [Theory]
        [InlineData(1)]
        public void Should_Be_Ok_When_Get_By_Id(int id) {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);
                var result = controller.GetById(id);
                var expected = service.Get(s => s.Id == id).FirstOrDefault();

                Assert.IsType<OkObjectResult>(result.Result);
                result = (result.Result as OkObjectResult).Value as LogDTO;
                result.Value.Title.Should().Be(expected.Title);
            }
        }

        [Theory]
        [InlineData(60)]
        public void Should_Be_Not_Found_When_Get_By_Id(int id) {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);
                var result = controller.GetById(id);

                Assert.IsType<NotFoundObjectResult>(result.Result);
                result.Value.Should().BeNull();
            }
        }

        [Fact]
        public void Should_Be_OK_When_Post() {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);
                var form = new LogDTO() {
                    Title = "TitleTest",
                    Details = "detail",
                    LevelName = "Critical",
                    SystemName = "Ninja"
                };
                var result = controller.Post(form);
                var expected = service.Get(s => s.Title == form.Title).FirstOrDefault();
                Assert.IsType<CreatedResult>(result);
            }
        }

        [Fact]
        public void Should_Be_OK_When_Delete() {
            var fakeContext = new FakeContext();
            fakeContext.FillWithAll();

            using (ApplicationDbContext dbContext = new ApplicationDbContext(fakeContext.FakeOptions)) {
                var service = new LogService(dbContext);
                var controller = new LogController(service, new LevelService(dbContext), new SystemService(dbContext), fakeContext.Mapper);

                controller.Delete(1);
                var result = service.Get(s => s.Id == 1).FirstOrDefault();
                result.Should().BeNull();
            }
        }
    }
}
