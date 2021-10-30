
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RivitaBackend.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
