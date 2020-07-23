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
	public class UserControllerTest
	{
        private TestServer server;
        private TestServer authServer;

        public UserControllerTest() {
            Environment.SetEnvironmentVariable("JWT_ISSUER", "localhost");
            Environment.SetEnvironmentVariable("JWT_SECRET", "YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            Environment.SetEnvironmentVariable("DB_USER", "postgres");
            Environment.SetEnvironmentVariable("DB_NAME", "cde");
            Environment.SetEnvironmentVariable("DB_PORT", "5432");
            Environment.SetEnvironmentVariable("DB_PASSWORD", "example");
            Environment.SetEnvironmentVariable("DB_HOST", "localhost");

            var authBuilder = new WebHostBuilder().
                UseEnvironment("Development").
                UseStartup<Startup>();
            authServer = new TestServer(authBuilder);
            authServer.BaseAddress = new Uri("http://localhost:5000");

            var builder = new WebHostBuilder().
                UseEnvironment("Testing").
                ConfigureServices(services => {
                    services.Configure<JwtBearerOptions>("Bearer", jwtOpts => {
                        jwtOpts.BackchannelHttpHandler = authServer.CreateHandler();
                    });
                }).
                UseStartup<Startup>();

            server = new TestServer(builder);
            server.BaseAddress = new Uri("http://localhost:5000");
        }

        [Fact]
        public void Should_Has_Authorize_Attribute_On_User_Controller() {
            var attributes = typeof(UserController).
                GetCustomAttributes(false).
                Select(x => x.GetType().Name).
                ToList();
            Assert.Contains("AuthorizeAttribute", attributes);
        }

        private Dictionary<string, string> GetAuthParameters(AuthenticateRequestDTO credentials) {
            var parameters = new Dictionary<string, string>();
            parameters["name"] = credentials.Name;
            parameters["password"] = credentials.Password;
            return parameters;
        }

        private AuthenticateResponseDTO GetToken(AuthenticateRequestDTO credentials) {
            var parameters = GetAuthParameters(credentials);
            var jsonContent = JsonConvert.SerializeObject(parameters);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var client = authServer.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync("/api/user/authenticate", contentString).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<AuthenticateResponseDTO>().Result;
        }

        [Fact]
        public void Should_Admin_Be_Authorized_On_Route_User() {
            var token = GetToken(new AuthenticateRequestDTO() { Name = "admin", Password = "4dm1n" });
            Assert.NotNull(token);

            var client = server.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

            var actual = client.GetAsync("/api/user/1").Result;
            Assert.NotEqual(HttpStatusCode.Unauthorized, actual.StatusCode);
            Assert.NotEqual(HttpStatusCode.Forbidden, actual.StatusCode);
        }
    }
}
