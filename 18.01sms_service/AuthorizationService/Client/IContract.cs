using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Client
{
    [ServiceContract]
    interface IContract
    {
        [OperationContract]
        bool doesExist(string login);
        [OperationContract]
        bool SignIn(string login, string password);
        [OperationContract]
        bool SignUp(string login, string password);
    }
}
