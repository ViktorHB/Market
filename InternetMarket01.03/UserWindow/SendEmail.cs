using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserWindow
{
    static class SendEmail
    {
        public static void Send(String email, String name, String text, String image)
        {
            //  EmailService.EmailServiceClient emailSend = new EmailService.EmailServiceClient();
            bool sendEmailResult;
            bool sendEmailResultSpecified;

            localhostEmail.EmailService emailSend = new localhostEmail.EmailService();

            emailSend.SendEmail(email, name, text, image, out sendEmailResult, out sendEmailResultSpecified);
        }
    }
}
