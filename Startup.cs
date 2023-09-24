using Microsoft.AspNetCore.Builder;
using Travista.Controllers;
using Microsoft.Extensions.DependencyInjection;


namespace Travista
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<ImageController>();
            services.AddTransient<DestinationController>();

        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure middleware here
        }
    }
}
