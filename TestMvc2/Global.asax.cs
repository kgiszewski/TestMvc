using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestMvc2.Migrations;

namespace TestMvc2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _checkMigrations();
        }

        public void _checkMigrations()
        {
            var versionAppsettingKey = "MyApp:DbVersion";

            var dllVersion = MigrationHelper.GetDllVersion();

            var currentVersion = ConfigurationManager.AppSettings[versionAppsettingKey];

            if (currentVersion != dllVersion)
            {
                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                config.AppSettings.Settings.Remove(versionAppsettingKey);
                config.AppSettings.Settings.Add(versionAppsettingKey, dllVersion);
                config.Save();

                MigrationHelper.HandleMigrations(new Version(currentVersion));
            }
        }
    }
}
