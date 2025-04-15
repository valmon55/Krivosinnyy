using FKA.Krivosinnyy.Services.IServices;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace FKA.Krivosinnyy.Services
{
    public class EmailSender : IEmailSender
    {
        public void Sent(string email, string subject, string message)
        {
            var from = new MailAddress("freemart@yandex.ru", "Fedor");
            var to = new MailAddress(email, "");
            var m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = message; //"<h2>Письмо-тест для работы сайта</h2>";
            m.IsBodyHtml = true;
            var client = new SmtpClient();
            /// Яндекс
            client.Host = "smtp.yandex.ru";
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("freemart@yandex.ru", "zjjaevixqlwwcpnf");
            client.Send(m);

            ///// Mail не работает...
            //client.Host = "smtp.mail.ru";
            //client.Port = 25; // 465 ?
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("fedor_ka@mail.ru", "yXiSawzuWrdar1WrrPAi");
            //client.Send(m);

        }
    }
}
