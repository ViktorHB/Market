using System;

namespace RegistrationWindow
{
    class SmsLogic
    {

        internal void SendSms(String phone, String text)
        {
            bool Result;
            bool ResultSpecified;

            localhostSendSms.SmsService sms = new localhostSendSms.SmsService();

            try
            {
                sms.SendSms(phone, text, out Result, out ResultSpecified);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
