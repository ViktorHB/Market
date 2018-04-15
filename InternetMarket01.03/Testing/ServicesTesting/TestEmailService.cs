using NUnit.Framework;

namespace Testing.AuthTesting
{
    [TestFixture]
    class TestEmailService
    {
        [Test]
        public void SendEmailTest()
        {
            Assert.AreEqual(true, new EmailServiceReference.EmailServiceClient().SendEmail("viktor.hb1@gmail.com", "Vasja", "email testing", "https://www.google.com.ua/images/branding/googlelogo/2x/googlelogo_color_120x44dp.png"));
        }
    }
}
