using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SignUpService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SignUpService.svc or SignUpService.svc.cs at the Solution Explorer and start debugging.
    public class SignUpService : ISignUpService
    {
        private MarketModel _dataBase;
        const int USER_ID = 2;
        public SignUpService()
        {
            _dataBase = new MarketModel();
        }

        public int GetCode(string p)
        {
            Random r = new Random();
            return r.Next(1000, 9999);
        }

        public bool SignUp(string phone, string password, string email, int code)
        {
            try
            {
                HashMD5 md5 = new HashMD5();
                _dataBase.Users.Add(new Users { email = email, id_accessstatud = USER_ID, phone = phone, password = md5.HashPassword(password) });
                _dataBase.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
