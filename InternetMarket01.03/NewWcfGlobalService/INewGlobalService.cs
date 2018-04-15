using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INewGlobalService" in both code and config file together.
    [ServiceContract]
    public interface INewGlobalService
    {
        [OperationContract]
        bool MakeOrder(int p_id, string userphone, DateTime dt, string fullprice, string count);
    }
}
