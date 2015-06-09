using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YouthSailingClassifieds.Startup))]
namespace YouthSailingClassifieds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
