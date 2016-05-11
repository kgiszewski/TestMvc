using System.Net.Mail;
using System.Web.Mvc;
using TestMvc2.Helpers;
using TestMvc2.Models;

namespace TestMvc2.Services
{
    public class SimpleEmailService
    {
        public string GetRenderedTemplate(ControllerContext controllerContext, string pathToPartial, object model)
        {
            return TransformationHelper.RenderRazorViewToString(controllerContext, pathToPartial, model);
        }

        public void Send(SimpleMailMessage message)
        {
            using (var mailMessage = new MailMessage()
            {
                From = new MailAddress(message.From),
                Subject = message.Subject,
                IsBodyHtml = message.IsHtml
            })
            {

                //to
                mailMessage.To.Add(message.To);

                //cc
                if (!string.IsNullOrEmpty(message.Cc))
                {
                    mailMessage.CC.Add(message.Cc);
                }

                //bcc
                if (!string.IsNullOrEmpty(message.Bcc))
                {
                    mailMessage.Bcc.Add(message.Bcc);
                }

                mailMessage.Body = message.Body;

                var smtp = new SmtpClient();
                smtp.Send(mailMessage);
            }
        }
    }
}