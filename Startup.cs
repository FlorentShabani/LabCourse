using Microsoft.AspNetCore.Builder;

namespace Travista
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure middleware here
        }
    }
}
