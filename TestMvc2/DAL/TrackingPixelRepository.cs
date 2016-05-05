using System;
using DAL;
using TestMvc2.Helpers;
using TestMvc2.Models;

namespace TestMvc2.DAL
{
    public class TrackingPixelRepository
    {
        public static void AddCampaignSource(PetaPocoUnitOfWork unitOfWork, string campaign, string source)
        {
            var ip = "";

            try
            {
                ip = IpHelper.GetIpAddress();
            }
            catch (Exception ex)
            {
                //bad IP, log it or something
            }

            unitOfWork.Database.Insert(new TrackingPixelOpen()
            {
                Campaign = campaign,
                Source = source,
                IpAddress = ip,
                OpenedOn = DateTime.Now,
            });
        }

        public static int GetTotalForCampaignSource(PetaPocoUnitOfWork unitOfWork, string campaign, string source)
        {
            return unitOfWork.Database.ExecuteScalar<int>(@"
                SELECT COUNT(*) AS total
                FROM TrackingPixelOpens
                WHERE campaign = @0 AND source = @1
            ", campaign, source);
        }
    }
}