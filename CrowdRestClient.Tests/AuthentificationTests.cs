using NUnit.Framework;
using CrowdRestClient;
using CrowdRestClient.Models;
using RestSharp;

namespace CrowdRestClientTests
{
    [TestFixture]
    public class AuthentificationTests
    {
        [Test]
        public void AuthentificationTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);

            IRestResponse<User> response = cwd.AuthenticateUser(TestData.UserName, TestData.UserPassword);
            Assert.IsTrue(response.IsSuccessful);
            Assert.NotNull(response.Data);
        }
    }
}
