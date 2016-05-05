using DAL;
using TestMvc2.DAL;

namespace TestMvc2.Services
{
    public class TrackingPixelService
    {
        public void AddCampaignSource(string campaign, string source)
        {
            using (var uow = new PetaPocoUnitOfWork())
            {
                TrackingPixelRepository.AddCampaignSource(uow, campaign, source);

                uow.Commit();
            }
        }

        public int GetTotalForCampaignSource(string campaign, string source)
        {
            using (var uow = new PetaPocoUnitOfWork())
            {
                return TrackingPixelRepository.GetTotalForCampaignSource(uow, campaign, source);
            }
        }
    }
}