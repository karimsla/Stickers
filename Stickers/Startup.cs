using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stickers.Startup))]
namespace Stickers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
          //  ConfigureAuth(app);

        }


    }
}
