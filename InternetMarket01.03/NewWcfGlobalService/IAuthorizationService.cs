using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthorizationService" in both code and config file together.
    [ServiceContract]
    public interface IAuthorizationService
    {

        [OperationContract]
        bool DoesUserExist(string l); // login

        [OperationContract]
        string GetUserStatus(string l); // login

        [OperationContract]
        bool Login(string l, string p);

        [OperationContract]
        string GetPasswordHash(String login);
    }
}
