using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ClientWebService.Data;
using ClientWebService.Data.Models;
using ClientWebService.Extentions;
using ClientWebService.Repositories.Implementations;
using ClientWebService.Repositories.Interfaces;
using ClientWebService.Services;
using ClientWebService.Services.Implementations;
using ClientWebService.Services.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
//using Swashbuckle.AspNetCore.Swagger;
//Nswag
using NJsonSchema;
using NSwag.AspNetCore;

namespace ClientWebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services/*IServ Collection pour enregistrer les ervices*/)
        {
            //utilisée pour configurer les services 'ainsi DI' qui seront utilisés par l'application. 
            //Une fois que l'application est lancée et que la première requête est reçue, 
            //la ConfigureServiceméthode est utilisée,  s'exécute avant la methode Configuration
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ClientWSContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:DB_ClientWebService"]));

            services.AddTransient<IAdresseRepository, AdresseRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();


            services.ConfigureCors();//Appel d Extentions

            services.ConfigureIISIntegration();//Appel d Extentions

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               
            });

            // !!
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IContactService,ContactService>();
            services.AddTransient<IAdresseService, AdresseService>();

            services.AddScoped<IDBInitialisationService, DBInitialisationService>();
           
            // Enregistrer le service de cinvertion
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // Ajouter le Generateur de Docs Swagger
            //services.AddSwaggerGen(c => {
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Title = "API Gestions des Clients",
            //        Description = "Service Web développé en ASP.NET Core 2.2 pour la Gestion des Clients",
            //        Version = "v1.0",
            //        TermsOfService = "None",
            //        Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
            //        {
            //            Name = "Soukaina EL HAYOUNI",
            //            Email = "soukainaelhayouni@outlook.fr"
            //        }
            //    });

            //    // Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});

            //Nswag
            // Register the Swagger services
            services.AddSwaggerDocument();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v2";
                    document.Info.Title = "Gestions Clients API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Soukaina EL HAYOUNI",
                        Email = "soukainaelhayouni@outlook.fr",
                        Url = "https://instagram.com/elhayouni.soukaina"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }

    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Active le middleware de fichiers statiques :
            app.UseStaticFiles();

            // NSwag
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwaggerUi3();


            //est utilisée pour spécifier comment l’application répondra dans chaque requête HTTP. 
            //Ce qui implique que la Configureméthode est spécifique à la requête HTTP
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // Pour initilialiser la Base de Données
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDBInitialisationService>();
                dbInitializer.Initialiser();
                dbInitializer.AlimenterDonnees();
            }


            //cette ligne au lieu d'ajouter le dossier Extentions ...;

            /*  app.UseCors(o => o.AllowAnyOrigin().WithMethods("POST", "DELETE", "GET", "PUT").AllowAnyHeader().AllowCredentials());*/

            // Activer le Doc Swagger
            app.UseSwagger();
            // Activer swagger-ui en spécifiant un cible JSON
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gestions des Clients - version 1.0");
            //});
        }
    }
}
