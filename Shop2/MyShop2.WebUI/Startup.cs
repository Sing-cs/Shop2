using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShop2.WebUI.Startup))]
namespace MyShop2.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
