using System;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Cde.Tests
{
    public class PingControllerTest : BaseTest
    {
        private string baseRoute = "api";

        [Fact]
        public async Task PingRequest() {
            var response = await Client.GetAsync($"{baseRoute}/ping");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
