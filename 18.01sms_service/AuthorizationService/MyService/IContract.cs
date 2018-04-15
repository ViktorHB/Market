using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyService
{
    [ServiceContract]
    public interface IContract
    {
        [OperationContract]
        bool doesExist(string login);
        [OperationContract]
        bool SignIn(string login, string password, string code);
        [OperationContract]
        bool SignUp(string login, string password, string code);
        [OperationContract]
        void GetCode(string number);

    }
}
