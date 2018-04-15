using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;

namespace Testing.AuthTesting
{
    [TestFixture]
    class TestNewGlobalService
    {
        [Test]
        public void MakeOrderTest()
        {
            Assert.AreEqual(true, new NewGlobalServiceReference.NewGlobalServiceClient().MakeOrder(6, "a", DateTime.Now, "100500", "100500"));
            Thread.Sleep(5000);
            DataBase.MarketModel tmp_db = new DataBase.MarketModel();
            tmp_db.Orders.Remove(tmp_db.Orders.Where(x => x.count == 100500 && x.full_cost == 100500).SingleOrDefault());
            tmp_db.SaveChanges();
        }
    }
}
