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
using Microsoft.IdentityModel.Tokens;

namespace Cde.Tests
{
	public class BaseTest
	{
		protected HttpClient client { get; set; }
		private HttpClient _authClient { get; set; }

		private TestServer _server;
		private TestServer _authServer;

		public BaseTest() {
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
			_authServer = new TestServer(authBuilder);
			_authServer.BaseAddress = new Uri("http://localhost:5000");
			_authClient = _authServer.CreateClient();
			_authClient.DefaultRequestHeaders.Accept.Clear();
			_authClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var builder = new WebHostBuilder().
				UseEnvironment("Testing").
				ConfigureServices(services => {
					services.Configure<JwtBearerOptions>("Bearer", jwtOpts => {
						jwtOpts.BackchannelHttpHandler = _authServer.CreateHandler();
					});
				}).
				UseStartup<Startup>();
			_server = new TestServer(builder);
			_server.BaseAddress = new Uri("http://localhost:5000");

			client = _server.CreateClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		private Dictionary<string, string> GetAuthParameters(AuthenticateRequestDTO credentials) {
			var parameters = new Dictionary<string, string>();
			parameters["name"] = credentials.Name;
			parameters["password"] = credentials.Password;
			return parameters;
		}

		protected StringContent JsonParameters(Dictionary<string, string> parameters) {
			var jsonContent = JsonConvert.SerializeObject(parameters);
			var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return contentString;
		}

		protected AuthenticateResponseDTO GetToken(AuthenticateRequestDTO credentials) {
			var parameters = GetAuthParameters(credentials);
			var contentString = JsonParameters(parameters);
			var response = _authClient.PostAsync("/api/user/authenticate", contentString).Result;
			response.EnsureSuccessStatusCode();
			return response.Content.ReadAsAsync<AuthenticateResponseDTO>().Result;
		}
	}
}
