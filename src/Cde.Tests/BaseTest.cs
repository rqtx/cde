using Cde.Database;
using Cde.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cde.Tests
{
	public class BaseTest
	{
		protected HttpClient Client { get; set; }
		private TestServer _server;

		public BaseTest() {
			Environment.SetEnvironmentVariable("JWT_ISSUER", "localhost");
			Environment.SetEnvironmentVariable("JWT_SECRET", "YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			Client = _server.CreateClient();
		}
	}
}
