using System;
using System.Net;
using System.Net.Mail;

namespace HospitalMSServer.Helpers
{
    public static class VerificationHelper
    {
        private const string SMTP_SERVER = "smtp.yandex.ru";

        private const string EMAIL = "slash-slash-slash@yandex.ru";

        private const int VERIFICATION_LINK_LENGTH = 20;

        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GetRandomVerificationLink()
        {            
            var stringChars = new char[VERIFICATION_LINK_LENGTH];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = CHARS[random.Next(CHARS.Length)];
            }

            return new string(stringChars);
        }

        public static void SendVerificationEmail(string address, string verificationLink)
        {
            string body = "<a href=\"http://www.yourwebsitename.com/api/authentication/verify?verificationLink=" + verificationLink + "\">click here to verify</a>";
            SendMail(SMTP_SERVER, EMAIL, "5ObwKYw5SMThqqmfzYKO", address, "Verification", body);
        }

        private static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(new MailAddress(mailto));
                mailMessage.Subject = caption;
                mailMessage.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                {
                    mailMessage.Attachments.Add(new Attachment(attachFile));
                }
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = smtpServer;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(from.Split('@')[0], password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Mail.Send: " + ex.Message);
            }
        }
    }
}
