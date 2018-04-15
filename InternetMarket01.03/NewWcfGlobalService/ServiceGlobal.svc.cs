using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceGlobal : IServiceGlobal
    {
        private MarketModel _database;

        public ServiceGlobal()
        {
            _database = new MarketModel();
        }

        // Market : MakeOrder, etc
        public bool MakeOrder(Product p, string userphone, DateTime dt, string fullprice, string count)
        {
            Orders order = new Orders
            {
                id_product = p.id,
                id_user = _database.Users.Single(u => u.phone == userphone).id,
                full_cost = double.Parse(fullprice),
                date = dt,
                count = int.Parse(count),
                id_state = 1
            };
            try
            {
                _database.Orders.Add(order);

                SendEmailToTheCustomer(order);

                _database.SaveChangesAsync();
                return true;
            }

            catch
            {
                return false;
            }
        }

        public void SendEmailToTheCustomer(Orders o)
        {

        }
    }
}
