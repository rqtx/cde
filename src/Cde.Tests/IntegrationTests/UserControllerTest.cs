using Cde.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Net.Http;
using System.Net;
using Cde.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Cde.Tests.IntegrationTests
{
	public class UserControllerTest : BaseTest
	{
        [Fact]
        public void Should_Has_Authorize_Attribute_On_User_Controller() {
            var attributes = typeof(UserController).
                GetCustomAttributes(false).
                Select(x => x.GetType().Name).
                ToList();
            Assert.Contains("AuthorizeAttribute", attributes);
        }

        [Fact]
        public void Should_Admin_Be_Authorized_On_Route_User() {
            var token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            Assert.NotNull(token);

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

            var actual = client.GetAsync("/api/user").Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
            Assert.NotEqual(HttpStatusCode.Unauthorized, actual.StatusCode);
            Assert.NotEqual(HttpStatusCode.Forbidden, actual.StatusCode);
        }

        [Fact]
        public void Onle_Admin_Authorized_On_Route_GetAllUsers() {
            var route = "/api/user";
            var token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            Assert.NotNull(token);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            var actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "vavilov", Password = "seeds" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.Forbidden, actual.StatusCode);

            client.DefaultRequestHeaders.Clear();
            token = GetToken(new AuthenticateRequestDTO() { Name = "VavilovSeeds", Password = "seeds" });
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
            actual = client.GetAsync(route).Result;
            Assert.Equal(HttpStatusCode.Forbidden, actual.StatusCode);
        }
    }
}
