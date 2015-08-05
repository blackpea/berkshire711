using html5up_dopetrope_mvc;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(html5up_dopetrope_mvc.Startup))]
namespace html5up_dopetrope_mvc
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}