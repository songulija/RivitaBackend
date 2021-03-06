using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RivitaBackend.Configurations;
using RivitaBackend.IRepository;
using RivitaBackend.Models;
using RivitaBackend.Repository;
using RivitaBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend
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
            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("dbConnection")));

            //adding AddMemoryCache to keep track who requested, what requested and ..
            //services.AddMemoryCache();

            //adding AddResponseCahcing
            //services.ConfigureHttpCacheHeaders();

            services.AddAuthentication();
            //calling method from ServiceExtensions to configure Identity
            //Configuration for JWT from ServiceExtensions. It requers to pass Configuration
            services.ConfigureJWT(Configuration);


            // adding Cors policy. so user from other networks could access our API. just adding policy with name
            //basically allowing here anybody and everybody to access this API
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
            });

            // Add autoMapper. For type providing MapperInitializer that i created in Configurations
            services.AddAutoMapper(typeof(MapperInitilizer));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //adding new serivice. IAuthManager mapped to AuthManager. AuthManager has methods implementation.
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RivitaBackend", Version = "v1" });
            });
            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 120
                });
            }).AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.ConfigureVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RivitaBackend v1"));

            app.ConfigurExceptionHandler();

            app.UseHttpsRedirection();

            // letting app know that it should use CORS policy with name "AllowAll" that i created 
            app.UseCors("AllowAll");

            //registering our middleware. to use ResponseCaching that i specified in ConfigureServices
            //app.UseResponseCaching();
            //app.UseHttpCacheHeaders();

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
