using ClientWebService.Data.Models;
using ClientWebService.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Data
{
    public class ClientWSContext : DbContext
    {
        public ClientWSContext(DbContextOptions<ClientWSContext> options) : base(options) { }

        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Client> Clients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict; //SetNull; //Foreign key properties are set to null
            }

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
