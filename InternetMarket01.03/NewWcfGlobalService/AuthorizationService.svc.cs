using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthorizationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuthorizationService.svc or AuthorizationService.svc.cs at the Solution Explorer and start debugging.
    public class AuthorizationService : IAuthorizationService
    {

        private MarketModel _dataBase;
        public AuthorizationService()
        {
            //_dataBase = new MarketEntities();
            _dataBase = new MarketModel();
        }

        public bool DoesUserExist(string login)
        {
            return _dataBase.Users.Any(u => u.phone == login);
        }

        public string GetPasswordHash(string login)
        {
            return _dataBase.Users.Single(x => x.phone == login).password;
        }

        public string GetUserStatus(string login)
        {
            var tmp_user = _dataBase.Users.Where(x => x.phone == login).SingleOrDefault();
            return _dataBase.Access.Where(x => x.id == tmp_user.id_accessstatud).SingleOrDefault().title;
        }

        public bool Login(string login, string password)
        {
            return new HashMD5().Connect(login, password);
        }
    }
}
