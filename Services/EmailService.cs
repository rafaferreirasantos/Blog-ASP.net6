using System.Net;
using System.Net.Mail;
using static Blog.Configuration;

namespace Blog.Services;
public class EmailService
{
    public bool Send(
      string toName,
      string toEmail,
      string subject,
      string body,
      string fromName = "Rafael Santos",
      string fromEmail = "osmarditto@hotmail.com"
      )
    {
        var smtp = new SmtpClient(Configuration.SMTP.Host, Configuration.SMTP.Port);
        smtp.Credentials = new NetworkCredential(Configuration.SMTP.User, Configuration.SMTP.Password);
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = false;

        var mail = new MailMessage();
        mail.From = new MailAddress(fromEmail, fromName);
        mail.To.Add(new MailAddress(toEmail, toName));
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        try
        {
            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}
