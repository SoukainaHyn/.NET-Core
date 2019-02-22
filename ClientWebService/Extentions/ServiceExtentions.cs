using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Extentions
{
    public static class ServiceExtentions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()// I can use :WithOrigins("http://www.something.com")
                    .AllowAnyMethod()// I can use : WithMethods("POST", "GET")
                    .AllowAnyHeader()// Ican use : WithHeaders("accept", "content-type")
                    .AllowCredentials());
            });
        }

        //configure an IIS integration which will help us with the IIS deployment
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
    }
}
