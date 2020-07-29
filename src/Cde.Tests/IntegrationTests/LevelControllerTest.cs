using Cde.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Cde.Tests.IntegrationTests
{
	public class LevelControllerTest
	{
        [Fact]
        public void Should_Has_Authorize_Attribute_On_Log_Controller() {
            var attributes = typeof(LevelController).
                GetCustomAttributes(false).
                Select(x => x.GetType().Name).
                ToList();
            Assert.Contains("AuthorizeAttribute", attributes);
        }
    }
}
