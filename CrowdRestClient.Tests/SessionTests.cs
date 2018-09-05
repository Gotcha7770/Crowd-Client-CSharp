using NUnit.Framework;
using CrowdRestClient;
using CrowdRestClient.Models;
using RestSharp;

namespace CrowdRestClientTests
{
    [TestFixture]
    public class SessionTests
    {
        [Test]
        public void AuthentificationTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);
            var context = new AuthenticationContext
            {
                UserName = TestData.UserName,
                Password = TestData.UserPassword
            };

            IRestResponse<string> response = cwd.AuthenticateUser(context);
            Assert.IsTrue(response.IsSuccessful);
            Assert.NotNull(response.Data);
        }

        [Test]
        public void TokenValidationTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);
            var context = new AuthenticationContext
            {
                UserName = TestData.UserName,
                Password = TestData.UserPassword
            };

            IRestResponse<string> response1 = cwd.AuthenticateUser(context);
            string token = response1.Data;

            IRestResponse response2 = cwd.IsTokenValid(token);
            Assert.IsTrue(response2.IsSuccessful);
        }

        [Test]
        public void GetSessionTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);
            var context = new AuthenticationContext
            {
                UserName = TestData.UserName,
                Password = TestData.UserPassword
            };

            IRestResponse<string> response1 = cwd.AuthenticateUser(context);
            string token = response1.Data;

            IRestResponse<SSOSession> response2 = cwd.GetSession(token);

            Assert.IsTrue(response2.IsSuccessful);
            Assert.NotNull(response2.Data);
        }

        [Test]
        public void FindUserFromTokenTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);
            var context = new AuthenticationContext
            {
                UserName = TestData.UserName,
                Password = TestData.UserPassword
            };

            IRestResponse<string> response1 = cwd.AuthenticateUser(context);
            string token = response1.Data;

            IRestResponse<User> response2 = cwd.FindUserFromToken(token);
            Assert.IsTrue(response2.IsSuccessful);
            Assert.NotNull(response2.Data);
            Assert.AreEqual(response2.Data?.Name, TestData.UserName);
        }

        [Test]
        public void InvalidateTokenTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);
            var context = new AuthenticationContext
            {
                UserName = TestData.UserName,
                Password = TestData.UserPassword
            };

            IRestResponse<string> response1 = cwd.AuthenticateUser(context);
            string token = response1.Data;

            Assert.IsTrue(response1.IsSuccessful);
            Assert.NotNull(token);

            IRestResponse response2 = cwd.InvalidateToken(token);

            Assert.IsTrue(response2.IsSuccessful);

            IRestResponse response3 = cwd.IsTokenValid(token);

            Assert.IsFalse(response3.IsSuccessful);
        }

        [Test]
        public void InvalidateTokenForUserTest()
        {
            var cwd = new CrowdClient(TestData.CrowdUri, TestData.AppName, TestData.AppPassword);
            var context = new AuthenticationContext
            {
                UserName = TestData.UserName,
                Password = TestData.UserPassword
            };

            IRestResponse<string> response1 = cwd.AuthenticateUser(context);
            string token = response1.Data;

            Assert.IsTrue(response1.IsSuccessful);
            Assert.NotNull(token);

            IRestResponse response2 = cwd.InvalidateTokensForUser(TestData.UserName);

            Assert.IsTrue(response2.IsSuccessful);

            IRestResponse response3 = cwd.IsTokenValid(token);

            Assert.IsFalse(response3.IsSuccessful);
        }
    }
}
