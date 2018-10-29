using AutoMapper;
using JetMovie.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JetMovie
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApplicationDbConnection(_configuration)
                .AddCustomServices()
                .AddAuthentication(_configuration)
                .AddAuthorizationWithIdentity()
                .AddAutoMapper()
                .AddSwagger()
                .AddMvc()
                .AddControllersAsServices()
                .AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCustomExceptionHandler()
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseCustomSwagger()
                .UseMvc();
        }
    }
}
