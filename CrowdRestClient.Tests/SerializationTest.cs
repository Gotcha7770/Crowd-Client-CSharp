using CrowdRestClient.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdRestClientTests
{
    [TestFixture]
    public class SerializationTest
    {
        [Test]
        public void TestMethod()
        {
            var authenticationContext = new AuthenticationContext {UserName = "SomeName", Password = "VeryStrongPassword" };
            string body = JsonConvert.SerializeObject(authenticationContext);

            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }
    }
}
