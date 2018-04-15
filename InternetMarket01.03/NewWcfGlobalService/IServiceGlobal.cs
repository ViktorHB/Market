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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceGlobal
    {

        [OperationContract]
        bool MakeOrder(Product p, string userphone, DateTime dt, string fullprice, string count);

        void SendEmailToTheCustomer(Orders o);
    }


}
