using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ServiceModel;
using Service;

namespace MyService
{
    public class Service : IContract
    {
        LoginDbEntities db;
        int code;

        public Service()
        {
            db = new LoginDbEntities();
        }

        public bool doesExist(string login)
        {
            if (db.Users.Any(x => x.phoneNumber == login))
                return true;

            return false;
        }
        
        public void GetCode(string number)
        {
            ChannelFactory<IContractMessage> channelFactory =
                new ChannelFactory<IContractMessage>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8000/Service"));

            IContractMessage proxy = channelFactory.CreateChannel();

            Random rand = new Random();
            code = rand.Next(1000, 9999);

            proxy.SendMessage(number, code.ToString());
        }

        public bool SignIn(string login, string password, string code_user)
        {
            Users uzya = db.Users.FirstOrDefault(x => x.phoneNumber == login && x.password == password);

            if (uzya != null && code == int.Parse(code_user))
                return true;

            return false;
        }

        public bool SignUp(string login, string password, string code_user)
        {
            Users uzya = new Users() { phoneNumber = login, password = password, roleId = 2 };

            if (code == int.Parse(code_user))
            {
                db.Users.Add(uzya);
                db.SaveChanges();

                return true;
            }

            return false;
        }


    }
}
