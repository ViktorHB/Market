using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NewWcfGlobalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmailService.svc or EmailService.svc.cs at the Solution Explorer and start debugging.
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }
        public bool SendEmail(string email, string name, string text, string image)
        {
            try
            {
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("vwowd.1@gmail.com", "Market");
                // кому отправляем
                MailAddress to = new MailAddress(email);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Спасибо за заказ!";
                // текст письма
                m.Body = $@"<h2>Дорогой, {name}!</h2><div><img style=""width:100px; heigth: 100px;"" src=""{image}"" alt=""product""></div><div><p style=""font-size:36px"">{text}</p></div>";
                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("vwowd.1@gmail.com", "1236987Q");
                smtp.EnableSsl = true;
                smtp.Send(m);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
