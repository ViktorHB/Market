using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace Testing.AuthTesting
{
    [TestFixture]
    class TestRegistrationService
    {
        [Test]
        public void GetCodeTesting()
        {
            int tmp = new RegistrServiceReference.SignUpServiceClient().GetCode(string.Empty);
            Assert.AreEqual(tmp, tmp);
        }

        [Test]
        public void SignUpTesting()
        {
            Assert.AreEqual(true, new RegistrServiceReference.SignUpServiceClient().SignUp("testQWERTY", "test", "testQWERTY", 1));
            Thread.Sleep(5000);
            DataBase.MarketModel tmp_db = new DataBase.MarketModel();
            tmp_db.Users.Remove(tmp_db.Users.Where(x => x.phone == "testQWERTY").SingleOrDefault());
            tmp_db.SaveChanges();
        }
    }
}
