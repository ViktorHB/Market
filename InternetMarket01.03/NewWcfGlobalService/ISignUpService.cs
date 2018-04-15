using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISignUpService" in both code and config file together.
    [ServiceContract]
    public interface ISignUpService
    {
        [OperationContract]

        int GetCode(string p); // phone number
        [OperationContract]

        bool SignUp(string phone, string password, string email, int code);
    }
}
