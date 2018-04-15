using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace SmsService
{
    [ServiceContract]
    interface IContractMessage
    {
        [OperationContract]
        void SendMessage(string number, string message);
    }
}
