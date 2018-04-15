using System;

namespace AuthorizationWindow
{
    class AuthLogic 
    {
        private AuthorizationServiceReference.AuthorizationServiceClient _service;
        public AuthLogic()
        {
            _service = new AuthorizationServiceReference.AuthorizationServiceClient();

        }

        internal bool Check(String login, String password) => _service.Login(login, password);
        internal string GetStatus(string login) => _service.GetUserStatus(login);
    }
}
