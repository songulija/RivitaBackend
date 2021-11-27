
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RivitaBackend.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RivitaBackend
{
    public static class ServiceExtensions
    {
        //in startup we will be able to just call methods directly
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //same we would add to Startup.cs. like services.AddIdentityCore we adding custom user class ApiUser
            //using lambda to customize certain things how it handles user interactions. set password policies
            var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            //creating new IdentityBuilder, userType to whatever was specified. there is also built in identity Role(user, or admin)
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //specify where it should store or which database for identity services to happen
            //passing DatabaseContext that we are using as our database and AddDefaultToken
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }

        // Configuration for JWT in Startup . We also need IConfiguration
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            //getting 'JWT" section from appsettings.json
            var jwtSettings = Configuration.GetSection("Jwt");
            //getting key that i set with Command Line
            var key = Environment.GetEnvironmentVariable("KEY");
            /*var issuer = Environment.GetEnvironmentVariable("Issuer");*/

            //basically adding authentication to app. and default scheme that i want  is JWT
            //when somebody tires to authenticate check for bearer token
            //then i set up parameters. ValidatieIssuer means we want to validate token. validate lifetime
            //and issuer key. Then we set ValidIssuer for any JWT token will be string from appsettings.json
            //then goes key that we hash. most important thing dont put key in appsettings.json
            //based on your situation you may need more validation
            //VALIDATE AUDIENCE TOO. to validate users
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                };
            });
        }

        public  static void ConfigurExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error => {

                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        Log.Error($"Something went Wrong in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please try agin later"

                        }.ToString()) ;
                    }
                });
            
            });
        }

        /// <summary>
        /// This is for Versioning configuration. I will add this to Startup.cs
        /// adding api versioning. setting options. when clients talk to api they will know what version they use
        /// default api version is 1 For example, you might allow clients to choose between passing 
        /// in a query string or a request header. The ApiVersionReader supports a static Combine method 
        /// that allows you to specify multiple ways to read versions. With this in place, clients get v2.0 of
        /// our API by calling /api/bands/4?version=2 or specifying a X-Api-Version header value of 2.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(op =>
            {
                op.ReportApiVersions = true;
                op.AssumeDefaultVersionWhenUnspecified = true;
                op.ApiVersionReader = ApiVersionReader.Combine(
                   new HeaderApiVersionReader("api-version"),
                   new QueryStringApiVersionReader("version"));
            });
        }


        /// <summary>
        /// adding ConfigureHttpCacheHeaders. i'll add this configuration to Startup.cs
        /// adding response caching, and http cache headers
        /// Its for global caching
        /// </summary>
        /// <param name="services"></param>
        /*
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            //setting expirationOptions to httpCacheHeaders
            //setting validationOpt MustRevalidate to true. means when data changed we go through that process again
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 65;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                }
            );
        
        }
        */
    }
}
