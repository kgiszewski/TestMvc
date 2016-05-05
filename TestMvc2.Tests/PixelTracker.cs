using DAL;
using NUnit.Framework;
using TestMvc2.Services;

namespace TestMvc2.Tests
{
    [TestFixture]
    [Category("Pixel Tracker")]
    public class PixelTracker
    {
        private TrackingPixelService _service = new TrackingPixelService();

        [TestFixtureSetUp]
        public void Setup()
        {
            PetaPocoUnitOfWork.ConnectionString = "testDb";
        }

        [TestCase("test", "test2")]
        [TestCase("test", "")]
        [TestCase("", "test2")]
        public void Can_Add_Pixel(string campaign, string source)
        {
            var count = _service.GetTotalForCampaignSource(campaign, source);

            _service.AddCampaignSource(campaign, source);

            var afterCount = _service.GetTotalForCampaignSource(campaign, source);

            Assert.AreEqual(afterCount, ++count);
        }
    }
}
