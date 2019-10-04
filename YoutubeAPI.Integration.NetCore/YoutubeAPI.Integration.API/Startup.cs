using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YoutubeAPI.Integration.Application.Services;
using YoutubeAPI.Integration.Domain.Interfaces;
using YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Interfaces;
using YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Services;
using YoutubeAPI.Integration.Infra.InternalServices;

namespace YoutubeAPI__Integration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // External Services Configuration
            services.AddSingleton(typeof(IGoogleService<>), typeof(GoogleService<>));
            services.AddSingleton<IYoutubeRepository, YoutubeRepository>();
            services.AddSingleton<IYoutubeServiceManager, YoutubeServiceManager>();
            services.AddScoped<IHomeApplicationService, HomeApplicationService>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
