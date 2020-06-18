using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace Cde.Tests
{
	public class BaseTest
	{
		protected HttpClient Client { get; set; }
		private TestServer _server;

		public BaseTest() {
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			Client = _server.CreateClient();
		}

	}
}
