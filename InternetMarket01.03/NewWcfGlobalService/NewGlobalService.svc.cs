using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NewGlobalService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select NewGlobalService.svc or NewGlobalService.svc.cs at the Solution Explorer and start debugging.
    public class NewGlobalService : INewGlobalService
    {
        private MarketModel _database;

        public NewGlobalService()
        {
            _database = new MarketModel();
        }

        // Market : MakeOrder, etc
        public bool MakeOrder(int p_id, string userphone, DateTime dt, string fullprice, string count)
        {
            Orders order = new Orders
            {
                id_product = p_id,
                id_user = _database.Users.Single(u => u.phone == userphone).id,
                full_cost = double.Parse(fullprice),
                date = dt,
                count = int.Parse(count),
                id_state = 1
            };
            try
            {
                _database.Orders.Add(order);
                _database.SaveChanges();
                return true;
            }

            catch(Exception)
            {
                return false;
            }
        }
    }
}
