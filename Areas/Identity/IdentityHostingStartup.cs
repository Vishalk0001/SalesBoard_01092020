using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SalesBoard.Areas.Identity.IdentityHostingStartup))]
namespace SalesBoard.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}