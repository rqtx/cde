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
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			Client = _server.CreateClient();
		}
	}
}
