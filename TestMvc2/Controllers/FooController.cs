using System.Web.Mvc;
using TestMvc2.Models;
using TestMvc2.Services;

namespace TestMvc2.Controllers
{
    public class FooController : Controller
    {
        public ActionResult Index()
        {
            var emailService = new SimpleEmailService();

            var renderedTemplate = emailService.GetRenderedTemplate(ControllerContext, "~/Views/MailTemplates/Template1.cshtml", new SampleEmailModel
            {
                Title = "Mr.",
                Name = "Willy Wonka"
            });

            emailService.Send(new SimpleMailMessage
            {
                To = "foo@bar.com",
                Cc = "bar@foo.com, yo@yo.com",
                From = "fancypants@anything.com",
                Bcc = "secret@bar.com",
                Subject = "Blah",
                Body = renderedTemplate,
                IsHtml = true
            });

            return View(new FooViewModel
            {
                Age = 38, 
                Name = "Kevin"
            });
        }

        [ChildActionOnly]
        public ActionResult Bar()
        {
            return PartialView("Bar", new FooViewModel
            {
                Name = "Bob",
                Age = 44
            });
        }
    }
}