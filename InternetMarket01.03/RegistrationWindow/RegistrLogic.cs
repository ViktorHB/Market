using System;

namespace RegistrationWindow
{
    class RegistrLogic
    {
        AuthorizationServiceReference.AuthorizationServiceClient sAuth;
        localhostRegistration.SignUpService sRegstr = new localhostRegistration.SignUpService();

        public RegistrLogic()
        {
            sAuth = new AuthorizationServiceReference.AuthorizationServiceClient();
        }

        internal bool CheckUserName(String login)
        {
            return sAuth.DoesUserExist(login);
        }

        internal void AddUser(string login, string password, string email)
        {
            bool Result;
            bool ResultSpecified;

            sRegstr.SignUp(login, password, email, 2, new bool(), out Result, out ResultSpecified);
        }
          
        
    }
}
