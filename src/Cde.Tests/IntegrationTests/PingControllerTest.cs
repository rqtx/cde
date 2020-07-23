using System;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Cde.Tests.IntegrationTests
{
    public class PingControllerTest : BaseTest
    {
        private string baseRoute = "api";

        [Fact]
        public async Task PingRequest() {
            var response = await client.GetAsync($"{baseRoute}/ping");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
