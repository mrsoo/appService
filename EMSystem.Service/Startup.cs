using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMSystem.Data.DbConfig;
using EMSystem.Resource.Repository.Repositories;
using EMSystem.Resource.Repository.Repositories.InterfaceDefine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EMSystem.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", config => {
                  config.Authority = "https://localhost:3000/";

                  config.Audience = "ApiService";
              });
            services.AddCors(confg =>
              confg.AddPolicy("AllowAll",
                  p => p.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));

            services.AddHttpClient();
            services.AddControllers();
            //add scope
            //services.AddScoped<IWebDbContext, WebDbContext>();
            //services.AddScoped<IDpmRepository, DpmRepository>();
            //services.AddScoped<IEmpRepository, EmpRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
