using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientWebService.Data.Models;
using ClientWebService.Data;
using ClientWebService.Services.Interfaces;

namespace ClientWebService.Services.Implementations
{
    public class DBInitialisationService : IDBInitialisationService
    {

        private readonly IServiceScopeFactory serviceScopeFactory;

        public DBInitialisationService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public void AlimenterDonnees()
        {
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ClientWSContext>())
                {
                    var client1 = new Client
                    {
                        Code = "C1",
                        Nom = "Client numero 1",
                        

                        Siret = "00000000000001",
                        Contact = new Contact
                        {
                            Code = "C1",
                            Nom = "Contact 01",
                            Prenom = "Prenom",
                            Fonction="fonction",
                            Service="service"
                            
                        },
                        Adresses = new List<Adresse>
                        {
                            new Adresse { Code="A1", Rue="14 rue", CodePostal="22222222",Email="fe@hhh.fr",Telephone="+212545487565",Portable="+212365485"}
                        },
                    };

                    //enregistrer le client
                    if (!context.Clients.Any())
                    {
                        context.Clients.Add(client1);
                        context.SaveChanges();
                    }

                }
            }
        }
        
        public void Initialiser()
        {
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ClientWSContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
