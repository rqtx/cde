using Cde.Controllers;
using Cde.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace Cde.Tests.IntegrationTests
{
	public class LogControllerTest : BaseTest
	{
        [Fact]
        public void Should_Has_Authorize_Attribute_On_Log_Controller() {
            var attributes = typeof(LogController).
                GetCustomAttributes(false).
                Select(x => x.GetType().Name).
                ToList();
            Assert.Contains("AuthorizeAttribute", attributes);
        }

        [Fact]
        public void All_Roles_Authorized_On_Route_GetAll() {
            var route = "/api/log/system/1";
            var token = GetToken(new AuthenticateRequestDTO() { Name = "VavilovSeeds", Password = "seeds" });
            Assert.NotNull(token);

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            var actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "vavilov", Password = "seeds" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
        }

        [Fact]
        public void All_Roles_Authorized_On_Route_GetByLevel() {
            var route = "/api/log/system/1/level/5";
            var token = GetToken(new AuthenticateRequestDTO() { Name = "VavilovSeeds", Password = "seeds" });
            Assert.NotNull(token);

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            var actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "vavilov", Password = "seeds" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
        }

        [Fact]
        public void All_Roles_Authorized_On_Route_GetOverview() {
            var route = "/api/log/overview/1";
            var token = GetToken(new AuthenticateRequestDTO() { Name = "VavilovSeeds", Password = "seeds" });
            Assert.NotNull(token);

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            var actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "vavilov", Password = "seeds" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
        }

        [Fact]
        public void All_Roles_Authorized_On_Route_GetById() {
            var route = "/api/log/1";
            var token = GetToken(new AuthenticateRequestDTO() { Name = "VavilovSeeds", Password = "seeds" });
            Assert.NotNull(token);

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            var actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "vavilov", Password = "seeds" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
        }

        [Fact]
        public void Only_Role_System_Authorized_On_Route_Post_To_Create_Log() {
            var route = "/api/log";
            var parameters = new Dictionary<string, string>();
            parameters["title"] = "error";
            parameters["details"] = "internal error";
            parameters["systemName"] = "VavilovSeeds";
            parameters["levelName"] = "Critical";

            var token = GetToken(new AuthenticateRequestDTO() { Name = "VavilovSeeds", Password = "seeds" });
            Assert.NotNull(token);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            var actual = client.PostAsync(route, JsonParameters(parameters)).Result;
            Assert.Equal(HttpStatusCode.Created, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "vavilov", Password = "seeds" });
            Assert.NotNull(token);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.PostAsync(route, JsonParameters(parameters)).Result;
            Assert.Equal(HttpStatusCode.Forbidden, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            Assert.NotNull(token);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.PostAsync(route, JsonParameters(parameters)).Result;
            Assert.Equal(HttpStatusCode.Forbidden, actual.StatusCode);
        }
    }
}
