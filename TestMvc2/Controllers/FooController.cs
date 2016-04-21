using System.Web.Mvc;
using TestMvc2.Models;

namespace TestMvc2.Controllers
{
    public class FooController : Controller
    {
        public ActionResult Index()
        {
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