using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMS_Server.Startup))]
namespace SMS_Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
