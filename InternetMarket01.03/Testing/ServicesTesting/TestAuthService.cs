using NUnit.Framework;

namespace Testing.AuthTesting
{

    [TestFixture]
    class TestAuthService
    {
        [Test]
        public void CheckLoginAndPasswordTest()
        {
            Assert.AreEqual(true, new AuthServiceReference.AuthorizationServiceClient().Login("a", "a"));
        }

        [Test]
        public void GetStatudByLoginTest()
        {
            Assert.AreEqual("user", new AuthServiceReference.AuthorizationServiceClient().GetUserStatus("a"));
        }
    }
}
