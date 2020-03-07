using EMSystem.Data.DbConfig;
using EMSystem.Resource.Repository.Repositories;
using EMSystem.Resource.Repository.Repositories.InterfaceDefine;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EMSystem.Resource
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
            services.AddDbContext<WebDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", opt =>
               {
                   
                   opt.Authority = "http://localhost:3000";
                   opt.RequireHttpsMetadata = false;
                   opt.Audience = "ApiResx";
               
               });

            IdentityModelEventSource.ShowPII = true;
          
            services.AddCors(confg =>
                confg.AddPolicy("AllowAll",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));
            services.AddControllers();

            //add scope
            services.AddScoped<IWebDbContext, WebDbContext>();
            services.AddScoped<IEmpRepository, EmpRepository>();
            services.AddScoped<IDpmRepository, DpmRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
