using Launchpad.Core.Factories;
using Launchpad.Core.Managers;
using Launchpad.Core.Managers.Interfaces;
using Launchpad.Core.Services;
using Launchpad.Core.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace LaunchpadApi
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Launchpad API", Version = "v1" });
            });

            services.AddSingleton(Configuration);

            // Launchpad.Core services
            services.AddTransient<ILaunchpadManager, LaunchpadManager>();
            services.AddTransient<ILaunchpadService, LaunchpadService>();
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Launchpad API V1");
            });

            app.UseMvc();
        }
    }
}
