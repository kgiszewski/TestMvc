using System.Web.Mvc;
using TestMvc2.Services;

namespace TestMvc2.Controllers
{
    public class TrackingPixelController : Controller
    {
        private TrackingPixelService _service = new TrackingPixelService();

        public ActionResult GetPixel(string campaign, string source)
        {
            if (!string.IsNullOrEmpty(campaign) || !string.IsNullOrEmpty(source))
            {
                _service.AddCampaignSource(campaign, source);
            }
            else
            {
                //log the bad input
            }

            return File("~/assets/images/pixel.png", "image/png");
        }
    }
}