using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestMvc2.Startup))]
namespace TestMvc2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
