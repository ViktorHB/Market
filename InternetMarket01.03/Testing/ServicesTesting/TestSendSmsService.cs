
using NUnit.Framework;

namespace Testing.AuthTesting
{
    [TestFixture]
    class TestSendSmsService
    {
        [Test]
        public void SendSmsTesting()
        {
            Assert.AreEqual(true, new SendSmsServiceReference.SmsServiceClient().SendSms("test", "test"));
        }
    }
}
